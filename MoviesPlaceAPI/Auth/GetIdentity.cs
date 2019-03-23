using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesPlaceAPI.Auth
{
  public class GetIdentity
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public GetIdentity(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
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

    public async Task<ClaimsIdentity> GenerateClaimsIdentity(User user)
    {
      // Create claims List
      var claims =  new List<Claim>()
      {
        new Claim(Constants.Strings.JwtClaimIdentifiers.Id, user.Id),
        new Claim(Constants.Strings.JwtClaimIdentifiers.UserName, user.UserName)
      };

      // Retrieve user claims
      var userClaims = await _userManager.GetClaimsAsync(user);
      // Retrieve user roles
      var userRoles = await _userManager.GetRolesAsync(user);

      claims.AddRange(userClaims);
      foreach(var userRole in userRoles){
        claims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Role, userRole));
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

    public async Task<ClaimsIdentity> GetClaimsIdentityForNewUser(User user)
    {
      // Add new user to the SuperUser Role. This is only to allow each new user to excercise
      // all CRUD actions 
      var result = await _userManager.AddToRoleAsync(user, "SUPERUSER");

      if(!result.Succeeded){
        return await Task.FromResult<ClaimsIdentity>(null);
      }

      return await this.GenerateClaimsIdentity(user);

    }

  }
}