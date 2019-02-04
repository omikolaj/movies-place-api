using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class CommentConfiguration
  {
    public CommentConfiguration(EntityTypeBuilder<Comment> model)    
    {
      model.HasKey(c => new { c.PostID, c.UserID });

      model.HasOne(c => c.User)
        .WithMany(u => u.Comments)
        .HasForeignKey(c => c.UserID);

      model.HasOne(c => c.Post)
        .WithMany(p => p.Comments)
        .HasForeignKey(c => c.PostID);
    }
  }
}