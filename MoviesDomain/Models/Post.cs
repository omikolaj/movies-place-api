using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Post
  {
    public enum RatingTypes
    {
      Bad = 1,
      OK = 2,
      Decent = 3,
      Great = 4,
      Excellent = 5
    }
    public int? PostID { get; set; }        
    public string UserID { get; set; }
    public User User { get; set; } 
    [Required, StringLength(100, MinimumLength = 4)]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }    
    [Required]
    public int Rating { get; set; }
    public string MoviePictureURL { get; set; }
    public string MoviePictureID { get; set; } = "1";
    public DateTime PostDate { get; set; }    
    public IEnumerable<Comment> Comments { get; set; }      
    public int MovieID { get; set; }
    public Movie Movie { get; set; }
  }
}