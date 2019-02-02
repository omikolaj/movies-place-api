using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class MoviePost
  {
      public int MoviePostID { get; set; }
      public int MovieID { get; set; }
      //Belongs to Post
      public int PostID { get; set; }
      [Required]
      public RatingTypes Rating { get; set; }
      public enum RatingTypes
      {
        Bad = 1,
        OK = 2,
        Decent = 3,
        Great = 4,
        Excellent = 5
      }
  }
}