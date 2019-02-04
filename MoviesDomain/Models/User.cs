using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.Models
{
  public class User
  {
    public int UserID { get; set; }
    [Required, StringLength(25, MinimumLength = 4)] 
    public string Username { get; set; }
    [Required, EmailAddress]
    public string Email{ get; set; }
    [Required]
    public int Password { get; set; }      
    public IEnumerable<Comment> Comments { get; set; }      
    public IEnumerable<Post> Posts { get; set; }    
    public IEnumerable<Favorite> Favorites { get; set; }
  }
}