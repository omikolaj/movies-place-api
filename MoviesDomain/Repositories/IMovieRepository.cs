using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Models;

namespace MoviesDomain.Repositories
{
  public interface IMovieRepository : IDisposable
  {
    Task<Movie> GetByMoviePostIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<Movie> AddAsync(Movie movie, CancellationToken ct = default(CancellationToken));

  }
}