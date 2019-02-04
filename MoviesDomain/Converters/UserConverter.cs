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
      userViewModel.UserID = user.UserID;
      userViewModel.Username = user.Username;
      userViewModel.Password = user.Password;
      userViewModel.Email = user.Email;
      
      return userViewModel;
    }

    public static List<UserViewModel> ConvertList(IEnumerable<User> users)
    {
      return users.Select(u =>
      {
        UserViewModel userViewModel = new UserViewModel();
        userViewModel.UserID = u.UserID;
        userViewModel.Username = u.Username;
        userViewModel.Password = u.Password;
        userViewModel.Email = u.Email;

        return userViewModel;
      }).ToList();
    }
  }
}