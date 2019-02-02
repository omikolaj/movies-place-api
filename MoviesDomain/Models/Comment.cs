using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Comment
  {
    public int CommentID { get; set; }  
    [Required]  
    public string Content { get; set; }
    //belongs_to post
    public int PostID { get; set; }
    //belongs_to user
    public int UserID { get; set; }
    public IEnumerable<UserComment> UserComments { get; set; }
    public User User { get; set; }
    public Post Post { get; set; }
  }
}