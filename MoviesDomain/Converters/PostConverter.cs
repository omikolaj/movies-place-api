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
      
      return postViewModel;
    }

    public static List<PostViewModel> ConvertList(IEnumerable<Post> posts)
    {
      return posts.Select(m => 
      {
        PostViewModel postViewModel = new PostViewModel();
        postViewModel.Description = m.Description;        
        postViewModel.PostDate = m.PostDate;
        postViewModel.PostID = m.PostID;
        postViewModel.UserID = m.UserID;
        return postViewModel;
      }).ToList();
    }
  }
}