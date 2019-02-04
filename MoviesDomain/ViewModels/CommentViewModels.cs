using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.ViewModels
{
  public class CommentViewModel
  {
    public int CommentID { get; set; }
    public string Content { get; set; }      
    public int PostID { get; set; }
    public PostViewModel Post { get; set; }
    public int UserID { get; set; }        
    public UserViewModel User { get; set; }
    
  }
}