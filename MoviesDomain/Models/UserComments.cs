using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class UserComment
  {
    public int UserCommentID { get; set; }    
    public string Content { get; set; }
    //belongs_to comment
    public int CommentID { get; set; }
    //belongs_to user
    public int UserID { get; set; }
    public Comment Comment { get; set; }

  }
}