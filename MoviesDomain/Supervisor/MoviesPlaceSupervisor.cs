using MoviesDomain.Repositories;

namespace MoviesDomain.Supervisor
{
  public partial class MoviesPlaceSupervisor : IMoviesPlaceSupervisor
  {
    private readonly ICommentRepository _commentRepository;
    private readonly IFavoritesRepository _favoritesRepository;
    private readonly IMoviePostRepository _moviePostRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserCommentRepository _userCommentRepository;
    private readonly IUserFavoritesRepository _userFavoritesRepository;
    private readonly IUserRepository _userRepository;

    public MoviesPlaceSupervisor()
    {
        
    }

    public MoviesPlaceSupervisor(ICommentRepository commentRepository,
        IFavoritesRepository favoritesRepository,
        IMoviePostRepository moviePostRepository,
        IMovieRepository movieRepository,
        IPostRepository postRepository,
        IUserCommentRepository userCommentRepository,
        IUserFavoritesRepository userFavoritesRepository,
        IUserRepository userRepository
    )
    {
        _commentRepository = commentRepository;
        _favoritesRepository = favoritesRepository;
        _moviePostRepository = moviePostRepository;
        _movieRepository = movieRepository;
        _postRepository = postRepository;
        _userCommentRepository = userCommentRepository;
        _userFavoritesRepository = userFavoritesRepository;
        _userRepository = userRepository;
    }

  }
}