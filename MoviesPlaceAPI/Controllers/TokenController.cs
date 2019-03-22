using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.Supervisor;
using MoviesPlaceAPI.Auth;
using MoviesPlaceAPI.Utilities;
using Newtonsoft.Json;

namespace MoviesPlaceAPI.Controllers
{
  public class TokenController : MoviesPlaceBaseController
  {    
    private readonly Tokens _tokens;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions; 
    private readonly UserManager<User> _userManager;
    private readonly GetIdentity _getIdentity;
    public TokenController(IMoviesPlaceSupervisor moviesPlaceSupervisor,
      Tokens tokens,
      IJwtFactory jwtFactory, 
      IOptions<JwtIssuerOptions> jwtOptions, 
      UserManager<User> userManager,
      GetIdentity getIdentity) : base(moviesPlaceSupervisor) 
    {
      _tokens = tokens;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
      _userManager = userManager;
      _getIdentity = getIdentity;
    }

    [HttpGet("refresh")]
    [AllowAnonymous]
    [ServiceFilter(typeof(CookieFilter))]
    public async Task<ActionResult> Refresh()
    {
      var tokenJSON = Request.Cookies.SingleOrDefault(c => c.Key == "token").Value;

      var token = JsonConvert.DeserializeObject(tokenJSON, typeof(AccessToken)) as AccessToken;
      
      var refreshToken = token.refresh_token;

      var principal = _tokens.GetPrincipalFromExpiredToken(token.token);
      var username = principal.Identity.Name;

      var user = _userManager.Users.Include(u => u.RefreshToken).SingleOrDefault(u => u.UserName == username);
      
      if(user == null || user.RefreshToken.Token != refreshToken) return BadRequest();

      ClaimsIdentity identity = await _getIdentity.GenerateClaimsIdentity(user);
      
      if(identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }

      var userView = UserConverter.Convert(user);

      if(await _moviesPlaceSupervisor.ValidRefreshTokenAsync(userView, refreshToken))
      {
        string refreshTokenNew = Tokens.GenerateRefreshToken();

        if (!await _moviesPlaceSupervisor.DeleteRefreshTokenAsync(userView, refreshToken))
        {
          return BadRequest(Errors.AddErrorToModelState("token_failure", "The refresh token did not match", ModelState));
        }

        await _moviesPlaceSupervisor.SaveRefreshTokenAsync(userView, refreshTokenNew);

        string newJwtToken = await Tokens.GenerateJwt(identity, _jwtFactory, userView.Username, _jwtOptions, refreshTokenNew, new JsonSerializerSettings
        {
          Formatting = Formatting.Indented
        });

        Response.Cookies.Delete("token");

        Response.Cookies.Append(
          "token",
          newJwtToken,
          new Microsoft.AspNetCore.Http.CookieOptions(){
          HttpOnly = true,
          SameSite = SameSiteMode.Strict,
          Expires = DateTime.Now.AddDays(5)
        });

        return new OkObjectResult(newJwtToken);

      }

      return BadRequest(Errors.AddErrorToModelState("token_failure", "Expired or invalid refresh token.", ModelState));

     
    }

    [HttpPost("revoke")]
    [Authorize]
    public async Task<ActionResult> Revoke()
    {
      var username = User.Identity.Name;

      var userView = UserConverter.Convert(_userManager.Users.SingleOrDefault(u => u.UserName == username));
      if(userView == null)
      {
        return BadRequest();
      }      

      userView.RefreshToken = null;

      await _moviesPlaceSupervisor.UpdateUserAsync(userView);

      return Ok();
    }
  }
}