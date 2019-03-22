using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesDomain.Models;
using MoviesDomain.Supervisor;
using MoviesPlaceAPI.Auth;

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