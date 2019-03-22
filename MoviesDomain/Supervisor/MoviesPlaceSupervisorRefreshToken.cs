using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
  {
    public async Task<bool> SaveRefreshTokenAsync(UserViewModel userViewModel, string token, CancellationToken ct = default(CancellationToken))
    {
      User user = await _userRepository.GetByIDAsync(userViewModel.UserID, ct);

      return await _tokenRepository.SaveAsync(user, token, ct);
    }

    public async Task<bool> DeleteRefreshTokenAsync(UserViewModel userViewModel, string token, CancellationToken ct = default(CancellationToken))
    {
      User user = await _userRepository.GetByIDAsync(userViewModel.UserID, ct);

      return await _tokenRepository.DeleteAsync(user, token, ct);
    }

    public async Task<bool> ValidRefreshTokenAsync(UserViewModel userViewModel, string token, CancellationToken ct = default(CancellationToken))
    {
      User user = await _userRepository.GetByIDAsync(userViewModel.UserID, ct);

      if(user == null) return false;
      
      return await _tokenRepository.ValidAsync(user, token, ct);
    }
  }
}