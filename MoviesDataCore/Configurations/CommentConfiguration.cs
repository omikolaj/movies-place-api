using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class CommentConfiguration
  {
    public CommentConfiguration(EntityTypeBuilder<Comment> model)    
    {
      model.HasKey(c => new { c.PostID, c.UserID });

      model.Property(c => c.Content)
        .IsRequired();

      model.HasOne(c => c.User)
        .WithMany(u => u.Comments)
        .HasForeignKey(c => c.UserID);

      model.HasOne(c => c.Post)
        .WithMany(p => p.Comments)
        .HasForeignKey(c => c.PostID);

      //Seed Data
      model.HasData(new Comment()
      { 
        CommentID = 1,
        Content = "I do not think that this movie was as good as you had rated it though",
        PostID = 1,
        UserID = "1",        
      }, new Comment()
      {
        CommentID = 2,
        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
        PostID = 1,
        UserID = "2",
      });
    }
  }
}