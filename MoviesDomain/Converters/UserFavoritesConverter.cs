using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class UserFavoritesConverter
  {
    public static UserFavoritesViewModel Convert(UserFavorites userFavorites)
    {
      UserFavoritesViewModel userFavoritesViewModel = new UserFavoritesViewModel();
      userFavoritesViewModel.MovieID = userFavorites.MovieID;
      userFavoritesViewModel.Note = userFavorites.Note;
      userFavoritesViewModel.UserID = userFavorites.UserID;
      
      return userFavoritesViewModel;
    }

    public static List<UserFavoritesViewModel> ConvertList(IEnumerable<UserFavorites> users)
    {
      return users.Select(uf =>
      {
        UserFavoritesViewModel userFavoritesViewModel = new UserFavoritesViewModel();
      userFavoritesViewModel.MovieID = uf.MovieID;
      userFavoritesViewModel.Note = uf.Note;
      userFavoritesViewModel.UserID = uf.UserID;
      
      return userFavoritesViewModel;
      }).ToList();
    }
  }
}