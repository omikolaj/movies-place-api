using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Favorite
  {
    public int FavoriteID { get; set; }
    public string Note { get; set; }
    [Display(Name = "UserID")]
    public int UserID { get; set; }
    [Display(Name = "MovieID")]
    public int MovieID { get; set; }    
    public Movie Movie { get; set; }
    public User User { get; set; }
  }

}