using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class PostConfiguration
  {
    public PostConfiguration(EntityTypeBuilder<Post> model)    
    {
      model.HasKey(p => p.PostID);

      model.HasOne(p => p.User)
        .WithMany(u => u.Posts)
        .HasForeignKey(p => p.UserID)
        .IsRequired();   

      model.HasOne(p => p.Movie)
        .WithMany(m => m.Posts)
        .HasForeignKey(p => p.MovieID)
        .IsRequired();
    }
  }
}