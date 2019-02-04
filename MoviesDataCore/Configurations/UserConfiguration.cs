using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class UserConfiguration
  {
    public UserConfiguration(EntityTypeBuilder<User> model)    
    {
      model.HasKey(u => u.UserID);
    }
  }
}