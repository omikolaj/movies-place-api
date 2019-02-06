using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class CommentRepository : ICommentRepository
  {
    private readonly MoviesPlaceContext _dbContext;
    public CommentRepository(MoviesPlaceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment> AddAsync(Comment newComment, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Comments.Add(newComment);
      await _dbContext.SaveChangesAsync(ct);
      return newComment;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      Comment commentToRemove = _dbContext.Comments.Find(ID);
      _dbContext.Comments.Remove(commentToRemove);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<Comment>> GetAllAsync(CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Comments.ToListAsync(ct);
    }

    public async Task<List<Comment>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Comments.Where(c => c.UserID == ID).ToListAsync(ct);
    }

    public async Task<Comment> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Comments.FindAsync(ID);
    }

    public async Task<bool> UpdateAsync(Comment comment, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Comments.Update(comment);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }
    
    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}