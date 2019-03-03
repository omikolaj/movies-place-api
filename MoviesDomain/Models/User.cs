using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MoviesDomain.Models
{
  public class User : IdentityUser
  {
    // public int UserID { get; set; }
    // [Required, StringLength(25, MinimumLength = 4)] 
    // public string Username { get; set; }
    // [Required, EmailAddress]
    // public string Email{ get; set; }
    // [Required, DataType(DataType.Password)]
    // public string Password { get; set; }      
    public IEnumerable<Comment> Comments { get; set; }      
    public IEnumerable<Post> Posts { get; set; }    
    public IEnumerable<Favorite> Favorites { get; set; }
  }
  
}