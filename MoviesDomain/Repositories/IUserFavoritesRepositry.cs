using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IUserFavoritesRepository : IDisposable
  {
    Task<List<UserFavorites>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<UserFavorites> GetByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<UserFavorites>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserFavorites> AddAsync(UserFavorites userFavorites, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(UserFavorites userFavorites, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(UserFavorites userFavorites, CancellationToken ct = default(CancellationToken));


  }
}