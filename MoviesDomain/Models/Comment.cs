using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Comment
  {
    public int CommentID { get; set; }
    [Required]
    public string Content { get; set; }
    [Display(Name = "PostID")]
    public int PostID { get; set; }
    public Post Post { get; set; }
    [Display(Name = "UserID")]
    public string UserID { get; set; }
    public User User { get; set; }
    
  }
}