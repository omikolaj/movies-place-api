using Microsoft.EntityFrameworkCore;
using MoviesDataCore.Configurations;
using MoviesDomain.Models;

namespace MoviesDataCore
{
  public class MoviesPlaceContext : DbContext
  {
    public MoviesPlaceContext(DbContextOptions<MoviesPlaceContext> options) : base(options) 
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Movie> Movies { get; set; }    
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      new CommentConfiguration(modelBuilder.Entity<Comment>());
      new FavoriteConfiguration(modelBuilder.Entity<Favorite>());
      new PostConfiguration(modelBuilder.Entity<Post>());
      new UserConfiguration(modelBuilder.Entity<User>());
      new MovieConfiguration(modelBuilder.Entity<Movie>());
    }
  }
}