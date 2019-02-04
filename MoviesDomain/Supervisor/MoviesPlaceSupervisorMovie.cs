using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
  {
    public async Task<MovieViewModel> GetMovieByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      MovieViewModel movieViewModel = MovieConverter.Convert(await _movieRepository.GetByIDAsync(ID, ct));
      movieViewModel.Favorites = await GetAllFavoritesByMovieIDAsync(movieViewModel.MovieID, ct);

      return movieViewModel;
    }

    public async Task<MovieViewModel> GetMovieByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      MovieViewModel movieViewModel = MovieConverter.Convert(await _movieRepository.GetByPostIDAsync(ID, ct));
      movieViewModel.Favorites = await GetAllFavoritesByMovieIDAsync(movieViewModel.MovieID, ct);

      return movieViewModel;
    }

    public async Task<MovieViewModel> AddMovieAsync(MovieViewModel movieViewModel, CancellationToken ct = default(CancellationToken))
    {
      Movie movie = new Movie()
      {
        Title = movieViewModel.Title,        
      };

      movie = await _movieRepository.AddAsync(movie, ct);
      movieViewModel.MovieID = movie.MovieID;

      return movieViewModel;
    }

    public async Task<bool> DeleteMovieAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _movieRepository.DeleteAsync(ID, ct);
    }

  }
}