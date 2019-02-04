using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class FavoriteConfiguration
  {
    public FavoriteConfiguration(EntityTypeBuilder<Favorite> model)    
    {
      model.HasKey(c => new { c.MovieID, c.UserID });

      model.HasOne(f => f.User)
        .WithMany(u => u.Favorites)
        .HasForeignKey(f => f.UserID);

      model.HasOne(f => f.Movie)
        .WithMany(m => m.Favorites)
        .HasForeignKey(f => f.MovieID);
    }
  }
}
