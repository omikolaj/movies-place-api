using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesPlace.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor 
  {
    public async Task<List<MoviePostViewModel>> GetAllMoviesPostsAsync(CancellationToken ct = default(CancellationToken))
    {    
      MoviePostViewModel moviePost = MoviePostConverter.ConvertList(await );
    }
  }
}