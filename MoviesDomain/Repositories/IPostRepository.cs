using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IPostRepository : IDisposable 
  {
    Task<List<Post>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<Post> GetByIDAsync(int? ID, CancellationToken ct = default(CancellationToken));

    Task<List<Post>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Post> AddAsync(Post post, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(Post post, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken));

  }
}