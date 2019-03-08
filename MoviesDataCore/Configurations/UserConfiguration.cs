using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class UserConfiguration
  {
    public UserConfiguration(EntityTypeBuilder<User> model)    
    {
      // model.HasKey(u => u.Id);

      // model.Property(u => u.UserName);

      // model.Property(u => u.Email);

      // model.Property(u => u.PasswordHash);

      model.HasData(new User()
      { 
        Id = "1",
        UserName = "scatman",
        Email = "scotty@gmail.com",
        PasswordHash = "password",        
      }, new User()
      {
        Id = "2",
        UserName = "gatesman",
        Email = "levi@gmail.com",
        PasswordHash = "password",  
      });
    }
  }
}