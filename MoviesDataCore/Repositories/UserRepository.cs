using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;
using MoviesDomain.Repositories;

namespace MoviesDataCore.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly MoviesPlaceContext _dbContext;
    public UserRepository(MoviesPlaceContext dbContext)
    {
      _dbContext = dbContext;
    }

    #region Private Methods

    private async Task<bool> UserExists(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await GetByIDAsync(ID, ct) != null;
    }

    #endregion

    public async Task<User> AddAsync(User newUser, CancellationToken ct = default(CancellationToken))
    {
      _dbContext.Users.Add(newUser);
      await _dbContext.SaveChangesAsync(ct);
      return newUser;
    }

    public async Task<bool> DeleteAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      if(!await UserExists(ID, ct)) return false;

      User userToDelete = _dbContext.Users.Find(ID);
      _dbContext.Users.Remove(userToDelete);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Users.ToListAsync(ct);
    }

    public async Task<User> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _dbContext.Users.FindAsync(ID);
    }

    public async Task<User> GetByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken ct = default(CancellationToken))
    {
      if(!await UserExists(user.UserID, ct)) return false;

      _dbContext.Users.Update(user);
      await _dbContext.SaveChangesAsync(ct);
      return true;
    }
    public void Dispose()
    {
      _dbContext.Dispose();
    }
  }
}