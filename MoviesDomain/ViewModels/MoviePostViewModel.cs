using System;
using System.ComponentModel.DataAnnotations;
using static MoviesDomain.Models.MoviePost;

namespace MoviesDomain.ViewModels
{
  public class MoviePostViewModel
  {
      public int MoviePostID { get; set; }
      public int MovieID { get; set; }
      //Belongs to Post
      public int PostID { get; set; }
      [Required]
      public RatingTypes Rating { get; set; }      
  }
}