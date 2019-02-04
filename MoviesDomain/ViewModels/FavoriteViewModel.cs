using System.Collections.Generic;

namespace MoviesDomain.ViewModels
{
  public class FavoriteViewModel
  {
    public int FavoriteID { get; set; }
    public int UserID { get; set; }
    public int MovieID { get; set; }
    public string Note { get; set; }    
  }

}