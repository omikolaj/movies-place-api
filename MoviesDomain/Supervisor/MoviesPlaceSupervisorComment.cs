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
    public async Task<List<CommentViewModel>> GetAllCommentsAsync(CancellationToken ct = default(CancellationToken))
    {
      List<CommentViewModel> comments = CommentConverter.ConvertList(await _commentRepository.GetAllAsync(ct));

      return comments;
    }

    public async Task<CommentViewModel> GetCommentByIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      CommentViewModel comment = CommentConverter.Convert(await _commentRepository.GetByIDAsync(ID));

      return comment;
    }

    public async Task<List<CommentViewModel>> GetAllCommentsByUserIDAsync(string ID, CancellationToken ct = default(CancellationToken))
    {
      List<CommentViewModel> comments = CommentConverter.ConvertList(await _commentRepository.GetAllByUserIDAsync(ID, ct));

      return comments;
    }

    public async Task<List<CommentViewModel>> GetAllCommentsByPostIDAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      List<CommentViewModel> comments = CommentConverter.ConvertList(await _commentRepository.GetAllByPostIDAsync(ID, ct));

      return comments;
    }

    public async Task<CommentViewModel> AddCommentAsync(CommentViewModel newCommentViewModel, CancellationToken ct = default(CancellationToken))
    {
      Comment comment = new Comment()
      {
        Content = newCommentViewModel.Content,        
        PostID = newCommentViewModel.PostID,
        UserID = newCommentViewModel.UserID.ToString()
      };

      comment = await _commentRepository.AddAsync(comment, ct);
      newCommentViewModel.CommentID = comment.CommentID;
      return newCommentViewModel;
    }

    public async Task<bool> UpdateCommentAsync(CommentViewModel commentViewModel, CancellationToken ct = default(CancellationToken))
    {
      Comment comment = await _commentRepository.GetByIDAsync(commentViewModel.CommentID, ct);

      // Check to make sure comment is not null
      if(comment == null) return false;

      comment.CommentID = commentViewModel.CommentID;
      comment.Content = commentViewModel.Content;
      comment.PostID = commentViewModel.PostID;
      comment.UserID = commentViewModel.UserID.ToString();

      return await _commentRepository.UpdateAsync(comment, ct);
    }

    public async Task<bool> DeleteCommentAsync(int ID, CancellationToken ct = default(CancellationToken))
    {
      return await _commentRepository.DeleteAsync(ID, ct);
    }

  }
}