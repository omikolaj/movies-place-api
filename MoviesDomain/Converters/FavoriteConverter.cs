using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class FavoritesConverter
  {
    public static FavoriteViewModel Convert(Favorite favorite)
    {
      FavoriteViewModel favoriteViewModel = new FavoriteViewModel();
      favoriteViewModel.FavoriteID = favorite.FavoriteID;
      favoriteViewModel.UserID = favorite.UserID;
      favoriteViewModel.MovieID = favorite.MovieID;
      favoriteViewModel.Note = favorite.Note;

      return favoriteViewModel;
    }

    public static List<FavoriteViewModel> ConvertList(IEnumerable<Favorite> favorites)
    {
      return favorites.Select(f =>
      {
        FavoriteViewModel favoriteViewModel = new FavoriteViewModel();
        favoriteViewModel.FavoriteID = f.FavoriteID;
        favoriteViewModel.UserID = f.UserID;
        favoriteViewModel.MovieID = f.MovieID;
        favoriteViewModel.Note = f.Note;
        return favoriteViewModel;
      }).ToList();
    }
  }
}