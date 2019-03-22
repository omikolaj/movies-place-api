using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviesDomain;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.Supervisor;
using MoviesDomain.ViewModels;
using MoviesPlaceAPI.Auth;
using Newtonsoft.Json;

namespace MoviesPlaceAPI.Controllers
{
  [Route("[controller]")]
  [Produces("application/json")]
  [AllowAnonymous]
  public class SessionController : MoviesPlaceBaseController
  {
    private readonly ILogger _logger;    
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;     
    private readonly GetIdentity _getIdentity;   
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signinManager;

    public SessionController(IMoviesPlaceSupervisor moviesPlaceSupervisor,
                              UserManager<User> userManager,
                              IJwtFactory jwtFactory, 
                              IOptions<JwtIssuerOptions> jwtOptions,
                              GetIdentity getIdentity,
                              ILogger<SessionController> logger,
                              SignInManager<User> signInManager) : base(moviesPlaceSupervisor)
    {          
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
      _userManager = userManager;
      _getIdentity = getIdentity;
      _signinManager = signInManager;
      _logger = logger;      
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel userLogin, CancellationToken ct = default(CancellationToken))
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      ClaimsIdentity identity = await _getIdentity.GetClaimsIdentity(userLogin.UserName, userLogin.Password);

      if(identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }

      var userView = UserConverter.Convert(_userManager.Users.Include(u => u.RefreshToken).SingleOrDefault(u => u.UserName == userLogin.UserName));

      // if(userView.RefreshToken != null)
      // {
      //   await _moviesPlaceSupervisor.DeleteRefreshTokenAsync(userView, userView.RefreshToken);
      // }

      string refreshToken = Tokens.GenerateRefreshToken();      

      await _moviesPlaceSupervisor.SaveRefreshTokenAsync(userView, refreshToken);
      
      string jwt = await Tokens.
                          GenerateJwt(identity, 
                          _jwtFactory, 
                          userLogin.UserName, 
                          _jwtOptions,
                          refreshToken, 
                          new JsonSerializerSettings { 
                            Formatting = Formatting.Indented 
                        });

      Response.Cookies.Append(
        "token",
        jwt,
        new Microsoft.AspNetCore.Http.CookieOptions(){
          HttpOnly = true,
          SameSite = SameSiteMode.Strict,
          Expires = DateTime.Now.AddDays(5)
        }
      );
      
      return new OkObjectResult(jwt);

    }
    
    [HttpDelete("logout")]    
    public async Task<ActionResult<bool>> Logout(CancellationToken ct = default(CancellationToken))
    {
      DeleteAllCookies(Request.Cookies.Keys);

      await _signinManager.SignOutAsync();

      return new OkObjectResult(true);
    }

    private void DeleteAllCookies(ICollection<string> cookies){
      foreach(string cookie in cookies){
        Response.Cookies.Delete(cookie);
      }
    }

  }
}