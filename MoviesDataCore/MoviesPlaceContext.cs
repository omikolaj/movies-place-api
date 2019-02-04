using Microsoft.EntityFrameworkCore;
using MoviesDomain.Models;

namespace MoviesDataCore
{
  public class MoviesPlaceContext : DbContext
  {
    public MoviesPlaceContext(DbContextOptions<MoviesPlaceContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Movie> Movies { get; set; }    
    public DbSet<Post> Posts { get; set; }
  }
}