using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class MovieConverter
  {
    public static MovieViewModel Convert(Movie movie)
    {
      MovieViewModel movieViewModel = new MovieViewModel();
      movieViewModel.MovieID = movie.MovieID;
      movieViewModel.Title = movie.Title;
      movieViewModel.MoviePostID = movie.MoviePostID;

      return movieViewModel;
    }

    public static List<MovieViewModel> ConvertList(IEnumerable<Movie> movies)
    {
      return movies.Select(m => 
      {
        MovieViewModel movieViewModel = new MovieViewModel();
        movieViewModel.MovieID = m.MovieID;
        movieViewModel.MoviePostID = m.MoviePostID;
        movieViewModel.Title = m.Title;
        return movieViewModel;
      }).ToList();
    }
  }
}