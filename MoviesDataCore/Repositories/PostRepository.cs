using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class PostRepository : IPostRepository 
  {
    private readonly MoviesPlaceContext _dbContext;
    public PostRepository(MoviesPlaceContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Private Methods

    private async Task<bool> PostExists(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(ID, ct) != null;
    }

    #endregion

    public async Task<Post> AddAsync(Post newPost, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Posts.Add(newPost);
      await _dbContext.SaveChangesAsync(ct);
      return newPost;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      if(!await PostExists(ID, ct)) return false;

      Post postToDelete = _dbContext.Posts.Find(ID);
      _dbContext.Posts.Remove(postToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<Post>> GetAllAsync(CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Posts.ToListAsync(ct);
    }

    public async Task<List<Post>> GetAllByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Posts.Where(p => p.UserID == ID).ToListAsync(ct);
    }

    public async Task<Post> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Posts.FindAsync(ID);
    }

    public async Task<bool> UpdateAsync(Post post, CancellationToken ct = default(CancellationToken))
    {
      if(!await PostExists(post.PostID, ct)) return false;

      _dbContext.Posts.Update(post);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}