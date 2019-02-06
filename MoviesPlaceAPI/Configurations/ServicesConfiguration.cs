using Microsoft.Extensions.DependencyInjection;
using MoviesDataCore.Repositories;
using MoviesDomain.Repositories;
using MoviesDomain.Supervisor;
using Newtonsoft.Json;

namespace MoviesPlaceAPI.Configurations
{
  public static class ServicesConfiguration
  {
    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
      services.AddScoped<ICommentRepository, CommentRepository>()
              .AddScoped<IFavoriteRepository, FavoriteRepository>()
              .AddScoped<IMovieRepository, MovieRepository>()
              .AddScoped<IPostRepository, PostRepository>()
              .AddScoped<IUserRepository, UserRepository>();

      return services;
    }

    public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
    {
      services.AddScoped<IMoviesPlaceSupervisor, MoviesPlaceSupervisor>();
      return services;
    }
    
    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
      services.AddMvc().AddJsonOptions(options => 
      {
        //Json.NET will ignore objects in reference loops and not serialize them. The first time an object is encountered it will be serialized as usual 
        //but if the object is encountered as a child object of itself the serializer will skip serializing it.
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      });

      return services;
    }

    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
      services.AddCors(options => 
      {
        options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .Build());
      });

      return services;
    }

  }
}