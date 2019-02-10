using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface ICommentRepository : IDisposable
  {
    Task<List<Comment>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<Comment> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<Comment>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<Comment>> GetAllByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken));    

    Task<Comment> AddAsync(Comment newComment, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(Comment comment, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken));
  }
}