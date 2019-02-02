using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class CommentConverter
  {
    public static CommentViewModel Converter(Comment comment)
    { 
      CommentViewModel commentViewModel = new CommentViewModel();
      commentViewModel.CommentID = comment.CommentID;      
      commentViewModel.Content = comment.Content;
      commentViewModel.PostID = comment.PostID;

      return commentViewModel;
    }

    public static List<CommentViewModel> ConvertList(IEnumerable<Comment> comments)
    {
      return comments.Select(c =>
      {
        CommentViewModel comment = new CommentViewModel();
        comment.CommentID = c.CommentID;
        comment.Content = c.Content;
        comment.PostID = c.PostID;
        return comment;
      }).ToList();
    }
  }

}