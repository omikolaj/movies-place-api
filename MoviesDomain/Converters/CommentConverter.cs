using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class CommentConverter
  {
    public static CommentViewModel Convert(Comment comment)
    { 
      CommentViewModel commentViewModel = new CommentViewModel();
      commentViewModel.CommentID = comment.CommentID;      
      commentViewModel.Content = comment.Content;
      commentViewModel.PostID = comment.PostID;
      commentViewModel.UserID = int.Parse(comment.UserID);
      commentViewModel.User = UserConverter.Convert(comment.User);
      return commentViewModel;
    }

    public static List<CommentViewModel> ConvertList(IEnumerable<Comment> comments)
    {
      return comments?.Select(c =>
      {
        CommentViewModel comment = new CommentViewModel();
        comment.CommentID = c.CommentID;
        comment.Content = c.Content;
        comment.PostID = c.PostID;
        comment.UserID = int.Parse(c.UserID);
        comment.Username = c.User.UserName;        
        return comment;
      }).ToList();
    }
  }

}