using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class Post
  {
    public int PostID { get; set; }   
    public int UserID { get; set; }
    public int MoviePostID { get; set; }
    [StringLength(300)]
    public string Description { get; set; }
    public DateTime PostDate { get; set; }
    //Has_many user_comments
    public IEnumerable<Comment> Comments { get; set; }
    public User User { get; set; }    
    public Movie Movie { get; set; }    
  }
}