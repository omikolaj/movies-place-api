using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoviesPlaceAPI.Auth;
using MoviesDomain.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesPlaceAPI.Auth
{
    public class Tokens
    {
      private readonly IConfiguration _configuration;
      
      public Tokens(IConfiguration configuration)
      {
        _configuration = configuration;
      }

      public static async Task<string> GenerateJwt(ClaimsIdentity identity, 
                                                    IJwtFactory jwtFactory,string userName, 
                                                    JwtIssuerOptions jwtOptions,
                                                    string refreshToken,
                                                    JsonSerializerSettings serializerSettings)
      {

        var response = new
        {
          id = identity.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id).Value,
          token = await jwtFactory.GenerateEncodedToken(userName, identity),
          expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
          refresh_token = refreshToken
        };

        return JsonConvert.SerializeObject(response, serializerSettings);
      }

      public static string GenerateRefreshToken()
      {
        var randomNumber = new byte[32];
        using(var rng = RandomNumberGenerator.Create())
        {
          rng.GetBytes(randomNumber);
          return Convert.ToBase64String(randomNumber);
        }
      }

      public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
      {
        var jwtAppSettingOptions = _configuration.GetSection(nameof(JwtIssuerOptions));

        var tokenValidationParameters = new TokenValidationParameters
        {
          ValidateAudience = true,
          ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
        
          ValidateIssuer = true,
          ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

          ValidateIssuerSigningKey = true,
          IssuerSigningKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions[nameof(JwtIssuerOptions.SigningKey)])),
          ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
        {
          throw new SecurityTokenException("Invalid token");
        }
        return principal;
      }
    }
}
