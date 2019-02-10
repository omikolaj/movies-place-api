using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Supervisor;

namespace MoviesPlaceAPI.Controllers
{
  [ApiController]
  public class MoviesPlaceBaseController : ControllerBase
  {
    protected readonly IMoviesPlaceSupervisor _moviesPlaceSupervisor;

    public MoviesPlaceBaseController(IMoviesPlaceSupervisor moviesPlaceSupervisor)    
    {
        _moviesPlaceSupervisor = moviesPlaceSupervisor;
    }
  }
}