using System.Collections.Generic;

namespace MoviesDomain.ViewModels
{
  public class FavoritesViewModel
  {
    public int FavoritesID { get; set; }
    //has_many user_favorites
    public IList<UserFavoritesViewModel> UserFavorites { get; set; }
    
  }

}