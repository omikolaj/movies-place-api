using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.Converters;
using MoviesDomain.Models;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
  {
    public async Task<List<PostViewModel>> GetAllPostsAsync(CancellationToken ct = default(CancellationToken))
    {
      List<PostViewModel> postsViewModel = PostConverter.ConvertList(await _postRepository.GetAllAsync(ct));     
           
      return postsViewModel;
    }

    public async Task<PostViewModel> GetPostByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      PostViewModel postViewModel = PostConverter.Convert(await _postRepository.GetByIDAsync(ID, ct));
      postViewModel.Movie = await GetMovieByIDAsync(postViewModel.MovieID, ct);
      postViewModel.User = await GetUserByIDAsync(postViewModel.UserID, ct);

      return postViewModel;
    }

    public async Task<List<PostViewModel>> GetAllPostsByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      List<PostViewModel> postsViewModel = PostConverter.ConvertList(await _postRepository.GetAllByUserIDAsync(ID, ct));
      postsViewModel.Select(async p => 
      {
        p.Movie = await GetMovieByIDAsync(p.MovieID, ct);
        p.User = await GetUserByIDAsync(p.UserID, ct);
      });      

      return postsViewModel;
    }

    public async Task<PostViewModel> AddPostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken))
    {
      Post post = new Post()
      {
        Title = postViewModel.Title,
        Description = postViewModel.Description,
        MovieID = postViewModel.MovieID,
        PostDate = new System.DateTime(),
        Rating = postViewModel.Rating, 
        UserID = postViewModel.UserID       
      };
      post = await _postRepository.AddAsync(post, ct);
      postViewModel.PostID = post.PostID;
      postViewModel.Movie = await GetMovieByIDAsync(postViewModel.MovieID, ct);
      postViewModel.User = await GetUserByIDAsync(postViewModel.UserID, ct);

      return postViewModel;
    }

    public async Task<bool> UpdatePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken))
    {
      Post post = await _postRepository.GetByIDAsync(postViewModel.PostID, ct);

      if(post == null) return false;

      post.Title = postViewModel.Title;
      post.Description = postViewModel.Description;
      post.MovieID = postViewModel.MovieID;
      post.Rating = postViewModel.Rating;
      post.MovieID = postViewModel.MovieID;      

      return await _postRepository.UpdateAsync(post, ct);
    }

    public async Task<bool> DeletePostAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _postRepository.DeleteAsync(ID, ct);
    }
  }
}