using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class MoviePostConverter
  {
    public static MoviePostViewModel Convert(MoviePost moviePost)
    {
      MoviePostViewModel moviePostViewModel = new MoviePostViewModel();
      moviePostViewModel.MoviePostID = moviePost.MoviePostID;
      moviePostViewModel.MovieID = moviePost.MovieID;
      moviePostViewModel.PostID = moviePost.PostID;
      moviePostViewModel.Rating = moviePost.Rating;
      
      return moviePostViewModel;
    }

    public static List<MoviePostViewModel> ConvertList(IEnumerable<MoviePost> moviePosts)
    {
      return moviePosts.Select(m => 
      {
        MoviePostViewModel moviePostViewModel = new MoviePostViewModel();
        moviePostViewModel.MovieID = m.MovieID;
        moviePostViewModel.MoviePostID = m.MoviePostID;
        moviePostViewModel.PostID = m.PostID;
        moviePostViewModel.Rating = m.Rating;
        return moviePostViewModel;
      }).ToList();
    }
  }
}