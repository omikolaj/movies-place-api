using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IFavoriteRepository : IDisposable
  {
    Task<List<Favorite>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<Favorite>> GetAllByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Favorite> GetByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Favorite> GetByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Favorite> AddAsync(Favorite favorite, CancellationToken ct = default(CancellationToken)); 

    Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken));
    
  }
}