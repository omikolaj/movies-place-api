using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviesDomain;
using MoviesDomain.Models;
using MoviesDomain.Supervisor;
using MoviesDomain.ViewModels;
using MoviesPlaceAPI.Auth;
using Newtonsoft.Json;

namespace MoviesPlaceAPI.Controllers
{
  [Route("[controller]")]
  [Produces("application/json")]
  public class SessionController : MoviesPlaceBaseController
  {
    private readonly ILogger _logger;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtIssuerOptions _jwtOptions;    
    private readonly RoleManager<IdentityRole> _roleManager;

    public SessionController(IMoviesPlaceSupervisor moviesPlaceSupervisor,
                              SignInManager<User> signInManager,
                              UserManager<User> userManage,
                              RoleManager<IdentityRole> roleManager,
                              IJwtFactory jwtFactory, 
                              IOptions<JwtIssuerOptions> jwtOptions,
                              ILogger<SessionController> logger) : base(moviesPlaceSupervisor)
    {
      _signInManager = signInManager;
      _userManager = userManage;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
      _logger = logger;
      _roleManager = roleManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginViewModel userLogin, CancellationToken ct = default(CancellationToken))
    {
      

      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      ClaimsIdentity identity = await GetClaimsIdentity(userLogin.UserName, userLogin.Password);

      if(identity == null)
      {
        return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
      }
      
      string jwt = await Tokens.
                          GenerateJwt(identity, 
                          _jwtFactory, 
                          userLogin.UserName, 
                          _jwtOptions, 
                          new JsonSerializerSettings { 
                            Formatting = Formatting.Indented 
                        });
      
      return new OkObjectResult(jwt);

    }
    private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        return await Task.FromResult<ClaimsIdentity>(null);

      // get the user to verifty
      var userToVerify = await _userManager.FindByNameAsync(userName);

      if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

      // check the credentials
      if (await _userManager.CheckPasswordAsync(userToVerify, password))
      {
        return await GenerateClaimsIdentity(userToVerify);
      }

      // Credentials are invalid, or account doesn't exist
      return await Task.FromResult<ClaimsIdentity>(null);
    }

    private async Task<ClaimsIdentity> GenerateClaimsIdentity(User user)
    {
      //new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"),

      IdentityOptions options = new IdentityOptions();      

      // Create claims List
      var claims =  new List<Claim>()
      {
        new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
        new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName)
      };

      // Retrieve user claims
      var userClaims = await _userManager.GetClaimsAsync(user);
      // Retrieve user roles
      var userRoles = await _userManager.GetRolesAsync(user);

      claims.AddRange(userClaims);
      foreach(var userRole in userRoles){
        claims.Add(new Claim(ClaimTypes.Role, userRole));
        var role = await _roleManager.FindByNameAsync(userRole);
        if(role != null){
          var roleClaims = await _roleManager.GetClaimsAsync(role);
          foreach(Claim roleClaim in roleClaims){
            claims.Add(roleClaim);
          }
        }
      }

      return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), claims);      
    }

  }
}