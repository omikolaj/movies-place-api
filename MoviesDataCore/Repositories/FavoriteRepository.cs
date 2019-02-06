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

    public async Task<Favorite> AddAsync(Favorite favorite, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Favorites.Add(favorite);
      await _dbContext.SaveChangesAsync(ct);
      return favorite;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      Favorite favToDelete = _dbContext.Favorites.Find(ID);
      _dbContext.Favorites.Remove(favToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<Favorite>> GetAllByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.Where(f => f.MovieID == ID).ToListAsync(ct);
    }

    public async Task<List<Favorite>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.Where(f => f.UserID == ID).ToListAsync(ct);
    }

    public async Task<List<Favorite>> GetAllByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Favorites.Where(f => f.FavoriteID == ID).ToListAsync(ct);
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}