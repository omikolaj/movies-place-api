using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoviesDomain.Models;

namespace MoviesDomain.ViewModels
{
  public class UserViewModel
  {
    public string UserID { get; set; }
    public string Username { get; set; }
    public string Email{ get; set; }
    public string Password { get; set; }  
    public RefreshToken RefreshToken { get; set; }
    public IList<CommentViewModel> Comments { get; set; }  
    public IList<PostViewModel> Posts { get; set; }
    public IList<FavoriteViewModel> Favorites { get; set; }
  }
}