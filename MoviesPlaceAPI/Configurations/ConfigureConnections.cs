using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesDataCore;
using MoviesDomain.DbInfo;

namespace MoviesPlaceAPI.Configurations
{
  public static class ConfigureConnections
  {
    public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
    {
      string connection = configuration.GetConnectionString("MoviesPlaceConnection");

      //Adds the MoviesPlaceContext to the DI container
      services.AddDbContextPool<MoviesPlaceContext>(options => options.UseNpgsql(connection));

      //services.AddDbContextPool<MoviesPlaceIdentityContext>(options => options.UseNpgsql(connection));

      //Adding connection info to DI incase needed later
      services.AddSingleton(new DbInfo(connection));

      return services;
    }
  }
}