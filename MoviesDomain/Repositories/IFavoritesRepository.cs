using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IFavoritesRepository : IDisposable
  {
    Task<List<Favorites>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<List<Favorites>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Favorites> AddAsync(Favorites favorites, CancellationToken ct = default(CancellationToken));    
    
  }
}