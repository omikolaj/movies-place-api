using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class FavoritesConverter
  {
    public static FavoritesViewModel Convert(Favorites favorites)
    {
      FavoritesViewModel favoritesViewModel = new FavoritesViewModel();
      favoritesViewModel.FavoritesID = favorites.FavoritesID;

      return favoritesViewModel;
    }

    public static List<FavoritesViewModel> ConvertList(IEnumerable<Favorites> favorites)
    {
      return favorites.Select(f =>
      {
        FavoritesViewModel favoritesViewModel = new FavoritesViewModel();
        favoritesViewModel.FavoritesID = f.FavoritesID;
        return favoritesViewModel;
      }).ToList();
    }
  }
}