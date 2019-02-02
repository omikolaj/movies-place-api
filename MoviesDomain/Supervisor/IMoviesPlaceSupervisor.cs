using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoviesDomain.ViewModels;

namespace MoviesDomain.Supervisor
{
  public interface IMoviesPlaceSupervisor
  {
    //Comments
    Task<List<CommentViewModel>> GetAllCommentsAsync(CancellationToken ct = default(CancellationToken));

    Task<CommentViewModel> GetCommentByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<CommentViewModel>> GetAllCommentsByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<CommentViewModel> AddCommentAsync(CommentViewModel newCommentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateCommentAsync(CommentViewModel commentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteCommentAsync(int ID, CancellationToken ct = default(CancellationToken));

    //Favorites
    Task<List<FavoritesViewModel>> GetAllFavoritesAsync(CancellationToken ct = default(CancellationToken));

    Task<List<FavoritesViewModel>> GetAllFavoritesByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<FavoritesViewModel> AddFavoriteAsync(FavoritesViewModel favoritesViewModel, CancellationToken ct = default(CancellationToken));    

    //MoviePost
    Task<List<MoviePostViewModel>> GetAllMoviesPostsAsync(CancellationToken ct = default(CancellationToken));

    Task<MoviePostViewModel> GetMoviePostByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<MoviePostViewModel> AddMoviePostAsync(MoviePostViewModel moviePostViewModel, CancellationToken ct = default(CancellationToken));

    //Movie
    Task<MovieViewModel> GetMovieByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<MovieViewModel> AddMovieAsync(MovieViewModel movieViewModel, CancellationToken ct = default(CancellationToken));

    //Post
    Task<List<PostViewModel>> GetAllPostsAsync(CancellationToken ct = default(CancellationToken));

    Task<PostViewModel> GetPostByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<PostViewModel>> GetAllPostsByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<PostViewModel> AddPostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdatePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeletePostAsync(PostViewModel postViewModel, CancellationToken ct = default(CancellationToken));

    //UserComments
    Task<List<UserCommentViewModel>> GetAllUsersCommentsAsync (CancellationToken ct = default(CancellationToken));

    Task<UserCommentViewModel> GetUserCommentByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<UserCommentViewModel>> GetAllUserCommentsByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserCommentViewModel> AddUserCommentAsync(UserCommentViewModel userCommentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateUserCommentAsync(UserCommentViewModel userCommentViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteUserCommentAsync(UserCommentViewModel userCommentViewModel, CancellationToken ct = default(CancellationToken));

    //UserFavorites
    Task<List<UserFavoritesViewModel>> GetAllUsersFavoritesAsync(CancellationToken ct = default(CancellationToken));

    Task<UserFavoritesViewModel> GetUserFavoritesByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<List<UserFavoritesViewModel>> GetAllUserFavoritesByUserIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserFavoritesViewModel> AddUserFavoritesAsync(UserFavoritesViewModel userFavoritesViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateUserFavoritesAsync(UserFavoritesViewModel userFavoritesViewModel, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteUserFavoritesAsync(UserFavoritesViewModel userFavoritesViewModel, CancellationToken ct = default(CancellationToken));

    //User
    Task<List<UserViewModel>> GetAllUsersAsync(CancellationToken ct = default(CancellationToken));

    Task<UserViewModel> GetUserByIDAsync(int ID, CancellationToken ct = default(CancellationToken));

    Task<UserViewModel> AddUserAsync(UserViewModel user, CancellationToken ct = default(CancellationToken));

    Task<bool> UpdateUserAsync(UserViewModel user, CancellationToken ct = default(CancellationToken));

    Task<bool> DeleteUserAsync(UserViewModel user, CancellationToken ct = default(CancellationToken));
  }
}