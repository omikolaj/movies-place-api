using System.Collections.Generic;

namespace MoviesDomain.Models
{
  public class UserFavorites
  {
    public int UserFavoritesID { get; set; }
    public int MovieID { get; set; }
    public int UserID { get; set; }
    public string Note { get; set; }
    
  }

}