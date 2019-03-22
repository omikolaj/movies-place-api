using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class FavoriteRepository : IFavoriteRepository
  {
    private readonly MoviesPlaceContext _dbContext;
    public FavoriteRepository(MoviesPlaceContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Private Methods

    private async Task<bool> FavoriteExists(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(ID, ct) != null;
    }

    #endregion

    public async Task<Favorite> AddAsync(Favorite favorite, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Favorites.Add(favorite);
      await _dbContext.SaveChangesAsync(ct);
      return favorite;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      if(!await FavoriteExists(ID, ct)) return false;

      Favorite favToDelete = _dbContext.Favorites.Find(ID);
      _dbContext.Favorites.Remove(favToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<Favorite>> GetAllByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.Where(f => f.MovieID == ID).ToListAsync(ct);
    }

    public async Task<List<Favorite>> GetAllByUserIDAsync(string ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.Where(f => f.UserID == ID).ToListAsync(ct);
    }

    public async Task<Favorite> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.FindAsync(ID);
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}