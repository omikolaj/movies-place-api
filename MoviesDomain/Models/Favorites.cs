using System.Collections.Generic;

namespace MoviesDomain.Models
{
  public class Favorites
  {
    public int FavoritesID { get; set; }
    //has_many user_favorites
    public IEnumerable<UserFavorites> UserFavorites { get; set; }
    
  }

}