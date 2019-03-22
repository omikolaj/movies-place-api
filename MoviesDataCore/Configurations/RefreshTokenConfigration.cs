using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations{
  public class RefreshTokenConfiguration
  {
    public RefreshTokenConfiguration(EntityTypeBuilder<RefreshToken> model)
    {
      // model.HasOne(rt => rt.User)
      //   .WithOne(u => u.RefreshToken)
      //   .HasForeignKey<User>();
    }    
  }
}