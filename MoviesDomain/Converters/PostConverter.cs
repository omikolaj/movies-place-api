using System.Collections.Generic;
using System.Linq;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Converters
{
  public static class PostConverter
  {
    public static PostViewModel Convert(Post post)
    {
      PostViewModel postViewModel = new PostViewModel();      
      postViewModel.Description = post.Description;      
      postViewModel.PostDate = post.PostDate;
      postViewModel.PostID = post.PostID;
      postViewModel.UserID = post.UserID;
      postViewModel.Title = post.Title;
      postViewModel.Rating = post.Rating;
      postViewModel.MovieID = post.MovieID;
      postViewModel.Comments = CommentConverter.ConvertList(post.Comments);
      postViewModel.User = UserConverter.Convert(post.User);
      postViewModel.Movie = MovieConverter.Convert(post.Movie);
      
      return postViewModel;
    }

    public static List<PostViewModel> ConvertList(IEnumerable<Post> posts)
    {
      return posts.Select(p => 
      {
        PostViewModel postViewModel = new PostViewModel();
        postViewModel.Description = p.Description;        
        postViewModel.MovieID = p.MovieID;
        postViewModel.PostDate = p.PostDate;
        postViewModel.PostID = p.PostID;
        postViewModel.UserID = p.UserID;
        postViewModel.Title = p.Title;
        postViewModel.Rating = p.Rating;
        postViewModel.Comments = CommentConverter.ConvertList(p.Comments);
        postViewModel.User = UserConverter.Convert(p.User);
        postViewModel.Movie = MovieConverter.Convert(p.Movie);
        return postViewModel;
      }).ToList();
    }
  }
}