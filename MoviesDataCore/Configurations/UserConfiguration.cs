using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class UserConfiguration
  {
    public UserConfiguration(EntityTypeBuilder<User> model)    
    {
      model.HasKey(u => u.UserID);

      model.Property(u => u.Username);

      model.Property(u => u.Email);

      model.Property(u => u.Password);

      model.HasData(new User()
      { 
        UserID = 1,
        Username = "scatman",
        Email = "scotty@gmail.com",
        Password = "*****",        
      }, new User()
      {
        UserID = 2,
        Username = "gatesman",
        Email = "levi@gmail.com",
        Password = "*****",  
      });
    }
  }
}