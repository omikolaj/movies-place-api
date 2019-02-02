using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IUserCommentRepository : IDisposable 
  {
    Task<List<UserComment>> GetAllAsync (CancellationToken ct = default(CancellationToken));

    Task<UserComment> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<UserComment>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserComment> AddAsync(UserComment userComment, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(UserComment userComment, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(UserComment userComment, CancellationToken ct = default(CancellationToken));

  }
}