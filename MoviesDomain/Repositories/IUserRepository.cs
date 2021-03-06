using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IUserRepository : IDisposable 
  {
    Task<List<User>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<User> GetByIDAsync(string ID, CancellationToken ct = default(CancellationToken));

    Task<User> AddAsync(User user, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateAsync(User user, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteAsync(string ID, CancellationToken ct = default(CancellationToken));
    
  }
}