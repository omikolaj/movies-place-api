using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class RefreshTokenRepository : IRefreshTokenRepository
  {
    private readonly MoviesPlaceContext _dbContext;
    public RefreshTokenRepository(MoviesPlaceContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task<bool> DeleteAsync(User user, string refreshToken, CancellationToken ct = default(CancellationToken))
    { 
      var userRefreshToken = _dbContext.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.UserId == user.Id);
      // If the refresh token we have in the database does not match the refresh token
      // passed in from the client, then this refresh token does not belong
      // to this user and we should not delete it. Something went wrong...

      if(user.RefreshToken.Token != userRefreshToken.Token) return false;            
        
      _dbContext.RefreshTokens.Remove(userRefreshToken);
      await _dbContext.SaveChangesAsync(ct); 
      return true;
    }

    public async Task<bool> SaveAsync(User user, string newRefreshToken, CancellationToken ct = default(CancellationToken))
    {
      // If the user being passed in already has a refresh token why would we create
      // and try to save another token? The expired refresh token should have been deleted first
      // user.RefreshToken should always be null if were trying to save a new refresh token
      if(user.RefreshToken != null){
        if(!await DeleteAsync(user, user.RefreshToken.Token, ct)) return false;
      };
      RefreshToken rt = new RefreshToken(newRefreshToken, DateTime.Now.AddDays(5), user.Id);

      await _dbContext.RefreshTokens.AddAsync(rt);      
      await _dbContext.SaveChangesAsync(ct);

      return true;
    }

    public async Task<bool> ValidAsync(User user, string refreshToken, CancellationToken ct = default(CancellationToken))
    {
      // The passed in refresh token did not match the one stored in the database
      if(user.RefreshToken.Token != refreshToken) return false;
      return _dbContext.RefreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);

    }
  }
}