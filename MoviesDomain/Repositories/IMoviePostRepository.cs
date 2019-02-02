using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IMoviePostRepository : IDisposable
  {
    Task<List<MoviePost>> GetAllAsync(CancellationToken ct = default(CancellationToken));

    Task<MoviePost> GetByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<MoviePost> AddAsync(MoviePost moviePost, CancellationToken ct = default(CancellationToken));
    
    
  }
}