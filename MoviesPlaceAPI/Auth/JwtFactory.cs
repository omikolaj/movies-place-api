using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MoviesDomain.Models;
using MoviesPlaceAPI.Auth;

namespace MoviesPlaceAPI.Auth
{
  public class JwtFactory : IJwtFactory
  {
    private readonly JwtIssuerOptions _jwtOptions;

    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);
    }

    public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
    {
      var claims = new[]
   {
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Role),
                 identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)
             };

      // Create the JWT security token and encode it.
      var jwt = new JwtSecurityToken(
          issuer: _jwtOptions.Issuer,
          audience: _jwtOptions.Audience, // Audience cannot be null
          claims: claims,
          notBefore: _jwtOptions.NotBefore,
          expires: _jwtOptions.Expiration,
          signingCredentials: _jwtOptions.SigningCredentials);

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      return encodedJwt;
    }

    // public ClaimsIdentity GenerateClaimsIdentity(User user)
    // {
    //   return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new[]
    //   {
    //       new Claim(Constants.Strings.JwtClaimIdentifiers.Id, user.Id),
    //       new Claim(Constants.Strings.JwtClaimIdentifiers.Role, Constants.Strings.JwtClaims.ApiAccess)
    //   });

    //   IdentityOptions _options = new IdentityOptions();
    //   var claims = new List<Claim>        
    //       { 
    //         new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
    //         new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
    //       };

    //   // var userClaims = await _userManager.GetClaimsAsync(user);
    //   // var userRoles = await _userManager.GetRolesAsync(user);
    //   // claims.AddRange(userClaims);
    //   // foreach (var userRole in userRoles)
    //   // {
    //   //   claims.Add(new Claim(ClaimTypes.Role, userRole));
    //   //   var role = await _roleManager.FindByNameAsync(userRole);
    //   //   if (role != null)
    //   //   {
    //   //     var roleClaims = await _roleManager.GetClaimsAsync(role);
    //   //     foreach (Claim roleClaim in roleClaims)
    //   //     {
    //   //       claims.Add(roleClaim);
    //   //     }
    //   //   }
    //   // }
    //   // return claims;
    // }

    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() -
                           new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                          .TotalSeconds);

    private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
      if (options == null) throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
      }

      if (options.JtiGenerator == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
      }
    }
  }
}