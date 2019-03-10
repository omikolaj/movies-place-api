using System.Security.Claims;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesPlaceAPI.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
    }
}