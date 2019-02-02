using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesDomain.ViewModels
{
  public class UserViewModel
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
    public IList<CommentViewModel> Comments { get; set; }  
    //has_many posts
    public IList<PostViewModel> Posts { get; set; }
    //has_many favorites
    public IList<FavoritesViewModel> Favorites { get; set; }
  }
}