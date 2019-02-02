using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;

namespace MoviesDataCore
{
  public class MoviesPlaceContext : DbContext
  {
    public MoviesPlaceContext(DbContextOptions<MoviesPlaceContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<MoviePost> MoviePost { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<UserFavorites> UserFavorites { get; set; }
    public DbSet<UserComment> UserComments { get; set; }
  }
}