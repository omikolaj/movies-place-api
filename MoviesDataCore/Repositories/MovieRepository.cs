using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class MovieRepository : IMovieRepository
  {
    private readonly MoviesPlaceContext _dbContext;
    public MovieRepository(MoviesPlaceContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Private Methods

    private async Task<bool> MovieExists(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(ID, ct) != null;
    }

    #endregion

    public async Task<Movie> AddAsync(Movie movie, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Movies.Add(movie);
      await _dbContext.SaveChangesAsync(ct);
      return movie;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      if(!await MovieExists(ID, ct)) return false;
      Movie movieToDelete = _dbContext.Movies.Find(ID);
      _dbContext.Movies.Remove(movieToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }    

    public async Task<Movie> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Movies.FindAsync(ID);
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }  
}