using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.ViewModels
{
  public class CommentViewModel
  {
    public int CommentID { get; set; }  
    //belongs_to post
    public int PostID { get; set; }
    //belongs_to user
    public int UserID { get; set; }
    public IList<UserCommentViewModel> UserComments { get; set; }
    public UserCommentViewModel User { get; set; }
    public PostViewModel Post { get; set; }
  }
}