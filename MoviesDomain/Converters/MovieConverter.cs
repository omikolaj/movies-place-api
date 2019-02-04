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

      return movieViewModel;
    }

    public static List<MovieViewModel> ConvertList(IEnumerable<Movie> movies)
    {
      return movies.Select(m => 
      {
        MovieViewModel movieViewModel = new MovieViewModel();
        movieViewModel.MovieID = m.MovieID;        
        movieViewModel.Title = m.Title;
        return movieViewModel;
      }).ToList();
    }
  }
}