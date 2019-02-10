using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesDomain.Models;
using static MoviesDomain.Models.Post;

namespace MoviesDomain.ViewModels
{
  public class PostViewModel
  {
    public int PostID { get; set; }   
    public int UserID { get; set; }   
    public string Username { get; set; } 
    public UserViewModel User { get; set; }
    public string Title { get; set; }  
    public string Description { get; set; } 
    public int Rating { get; set; }   
    public DateTime PostDate { get; set; }    
    public IList<CommentViewModel> Comments { get; set; }      
    public int MovieID { get; set; }
    public MovieViewModel Movie { get; set; }    
  }
}