using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IRefreshTokenRepository
  {
    Task<bool> DeleteAsync(User user, string refreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> SaveAsync(User user, string newRefreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> ValidAsync(User user, string refreshToken, CancellationToken ct = default(CancellationToken));
  }
}