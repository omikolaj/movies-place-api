using System.Collections.Generic;
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
      postViewModel.Movie = await GetMovieByPostIDAsync(postViewModel.PostID, ct);
      postViewModel.User = await GetUserByPostIDAsync(postViewModel.UserID, ct);

      return postViewModel;
    }

    public async Task<List<PostViewModel>> GetAllPostsByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {

    }

    public async Task<PostViewModel> AddPostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken))
    {

    }

    public async Task<bool> UpdatePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken))
    {

    }

    public async Task<bool> DeletePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken))
    {

    }
  }
}