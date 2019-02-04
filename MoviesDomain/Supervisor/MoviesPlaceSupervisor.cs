using MoviesDomain.Repositories;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public MoviesPlaceSupervisor()
    {
        
    }

    public MoviesPlaceSupervisor(ICommentRepository commentRepository,
        IFavoriteRepository favoriteRepository,
        IMovieRepository movieRepository,
        IPostRepository postRepository,
        IUserRepository userRepository
    )
    {
        _commentRepository = commentRepository;
        _favoriteRepository = favoriteRepository;
        _movieRepository = movieRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

  }
}