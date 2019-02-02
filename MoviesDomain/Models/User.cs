using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class User
  {
    public int UserID { get; set; }
    [Required]
    [StringLength(25, MinimumLength=4)] 
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email{ get; set; }
    public int Password { get; set; }  
    //has_many comments
    public IEnumerable<Comment> Comments { get; set; }  
    //has_many posts
    public IEnumerable<Post> Posts { get; set; }
    //has_many favorites
    public IEnumerable<Favorites> Favorites { get; set; }
  }
}