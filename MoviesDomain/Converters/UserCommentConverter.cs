using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class UserCommentConverter
  {
    public static UserCommentViewModel Convert(UserComment userComment)
    {
      UserCommentViewModel userCommentViewModel = new UserCommentViewModel();
      userCommentViewModel.UserCommentID = userComment.UserCommentID;
      userCommentViewModel.UserID = userComment.UserID;
      userCommentViewModel.Content = userComment.Content;
      
      return userCommentViewModel;
    }

    public static List<UserCommentViewModel> ConvertList(IEnumerable<UserCommentViewModel> userComments)
    {
      return userComments.Select(m =>
      {
        UserCommentViewModel userCommentViewModel = new UserCommentViewModel();
        userCommentViewModel.UserCommentID = m.UserCommentID;
        userCommentViewModel.UserID = m.UserID;
        userCommentViewModel.Content = m.Content;
        return userCommentViewModel;
      }).ToList();
    }
  }
}