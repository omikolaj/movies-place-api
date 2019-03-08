using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;
using static MoviesDomain.Models.Post;

namespace MoviesDataCore.Configurations
{
  public class PostConfiguration
  {
    public PostConfiguration(EntityTypeBuilder<Post> model)    
    {
      model.HasKey(p => p.PostID);

      model.Property(p => p.Rating);
        
      model.Property(p => p.Description);

      model.Property(p => p.Title);

      model.HasOne(p => p.User)
        .WithMany(u => u.Posts)
        .HasForeignKey(p => p.UserID)
        .IsRequired();   

      model.HasOne(p => p.Movie)
        .WithMany(m => m.Posts)
        .HasForeignKey(p => p.MovieID)
        .IsRequired();

      model.HasData(new Post(){
        PostID = 1,
        PostDate = DateTime.Now,
        UserID = "1",
        MovieID = 1,
        Title = "I really liked watching her laugh",
        Description = "Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance.",
        Rating = (int)RatingTypes.Great
      });
    }
  }
}