using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesDomain.Models;

namespace MoviesDataCore.Configurations
{
  public class MovieConfiguration
  {
    public MovieConfiguration(EntityTypeBuilder<Movie> model)    
    {
      model.HasKey(m => m.MovieID);

      model.Property(m => m.Title);

      model.HasData(new Movie()
      {
        MovieID = 1,
        Title = "Avangers",
      });
    }
  }
}