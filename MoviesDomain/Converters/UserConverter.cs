using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class UserConverter
  {
    public static UserViewModel Convert(User user)
    {
      UserViewModel userViewModel = new UserViewModel();
      userViewModel.UserID = user.Id;
      userViewModel.Username = user.UserName;
      userViewModel.Password = user.PasswordHash;
      userViewModel.Email = user.Email;
      userViewModel.RefreshToken = user.RefreshToken;
      
      return userViewModel;
    }

    public static List<UserViewModel> ConvertList(IEnumerable<User> users)
    {
      return users.Select(u =>
      {
        UserViewModel userViewModel = new UserViewModel();
        userViewModel.UserID = u.Id;
        userViewModel.Username = u.UserName;
        userViewModel.Password = u.PasswordHash;
        userViewModel.Email = u.Email;
        userViewModel.RefreshToken = u.RefreshToken;

        return userViewModel;
      }).ToList();
    }
  }
}