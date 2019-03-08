using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoviesDataCore;
using MoviesDomain.Models;

namespace MoviesPlaceAPI.Extensions
{
  public static class ApplicationBuilderExtensions 
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app) 
    { 
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider; 
        try 
        { 
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            ApplicationDbInitializer.SeedUsers(userManager, roleManager); 
         } 
        catch (Exception ex) 
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>(); 
            logger.LogError(ex, "An error occurred while seeding the database."); 
        } 
        return app; 
    } 

  }
}
