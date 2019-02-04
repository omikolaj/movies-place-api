using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Movie
  {
    public int MovieID { get; set; }    
    [Required]
    [StringLength(50, MinimumLength=3)]    
    public string Title { get; set; }
    public IEnumerable<Favorite> Favorites { get; set; }
    public IEnumerable<Post> Posts { get; set; }
  }
}