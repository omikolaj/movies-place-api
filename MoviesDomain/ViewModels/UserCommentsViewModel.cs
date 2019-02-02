using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.ViewModels
{
  public class UserCommentViewModel
  {
    public int UserCommentID { get; set; }
    [Required]  
    public string Content { get; set; }
    //belongs_to comment
    public int CommentID { get; set; }
    //belongs_to user
    public int UserID { get; set; }
    public CommentViewModel Comment { get; set; }

  }
}