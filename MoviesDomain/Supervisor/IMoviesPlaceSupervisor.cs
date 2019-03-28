using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public interface IMoviesPlaceSupervisor
  {
    //Comment
    #region Comment
    Task<List<CommentViewModel>> GetAllCommentsAsync(CancellationToken ct = default(CancellationToken));

    Task<CommentViewModel> GetCommentByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<CommentViewModel>> GetAllCommentsByUserIDAsync(string ID, CancellationToken ct = default(CancellationToken));

    Task<List<CommentViewModel>> GetAllCommentsByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<CommentViewModel> AddCommentAsync(CommentViewModel newCommentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateCommentAsync(CommentViewModel commentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteCommentAsync(int ID, CancellationToken ct = default(CancellationToken));

    #endregion

    //Favorite
    #region Favorite
    Task<List<FavoriteViewModel>> GetAllFavoritesByUserIDAsync(string ID, CancellationToken ct = default(CancellationToken));

    Task<List<FavoriteViewModel>> GetAllFavoritesByMovieIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<FavoriteViewModel> GetFavoriteByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<FavoriteViewModel> AddFavoriteAsync(FavoriteViewModel favoriteViewModel, CancellationToken ct = default(CancellationToken));

    #endregion

    //Movie
    #region Movie
    Task<MovieViewModel> GetMovieByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<MovieViewModel> GetMovieByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<MovieViewModel> AddMovieAsync(MovieViewModel movieViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteMovieAsync(int ID, CancellationToken ct = default(CancellationToken));

    #endregion

    //Post
    #region Post
    Task<List<PostViewModel>> GetAllPostsAsync(CancellationToken ct = default(CancellationToken));

    Task<PostViewModel>  GetPostByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<PostViewModel>> GetAllPostsByUserIDAsync(string ID, CancellationToken ct = default(CancellationToken));

    Task<PostViewModel> AddPostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdatePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeletePostAsync(int ID, CancellationToken ct = default(CancellationToken));

    #endregion

    //User
    #region User
    Task<List<UserViewModel>> GetAllUsersAsync(CancellationToken ct = default(CancellationToken));

    Task<UserViewModel> GetUserByIDAsync(string ID, CancellationToken ct = default(CancellationToken));

    Task<UserViewModel> GetUserByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserViewModel> AddUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateUserAsync(UserViewModel userViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteUserAsync(string ID, CancellationToken ct = default(CancellationToken));    

    #endregion

    #region RefreshToken

    Task<bool> DeleteRefreshTokenAsync(UserViewModel userViewModel, string refreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> SaveRefreshTokenAsync(UserViewModel userViewModel, string newRefreshToken, CancellationToken ct = default(CancellationToken));

    Task<bool> ValidRefreshTokenAsync(UserViewModel userViewModel, string refreshToken, CancellationToken ct = default(CancellationToken));

    #endregion

  }
}