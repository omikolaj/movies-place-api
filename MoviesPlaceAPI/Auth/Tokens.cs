using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoviesPlaceAPI.Auth;
using MoviesDomain.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace MoviesPlaceAPI.Auth
{
    public class Tokens
    {
      public static async Task<string> GenerateJwt(ClaimsIdentity identity, 
                                                    IJwtFactory jwtFactory,string userName, 
                                                    JwtIssuerOptions jwtOptions, 
                                                    JsonSerializerSettings serializerSettings)
      {

        IdentityOptions options = new IdentityOptions();
        var response = new
        {
          id = identity.Claims.Single(c => c.Type == options.ClaimsIdentity.UserIdClaimType).Value,
          token = await jwtFactory.GenerateEncodedToken(userName, identity),
          expires_in = (int)jwtOptions.ValidFor.TotalSeconds
        };

        return JsonConvert.SerializeObject(response, serializerSettings);
      }
    }
}