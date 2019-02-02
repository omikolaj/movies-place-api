using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Movie
  {
    [Required]
    public int MovieID { get; set; }
    //belongs_to MoviePost
    [Required]
    public int MoviePostID { get; set; }
    [Required]
    [StringLength(50, MinimumLength=3)]    
    public string Title { get; set; }
  }
}