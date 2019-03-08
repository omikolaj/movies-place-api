using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.RouteAnalyzer;
using GlobalErrorHandling.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesDataCore;
using MoviesDomain.Models;
using MoviesPlaceAPI.Configurations;
using MoviesPlaceAPI.Extensions;

namespace MoviesPlaceAPI
{
  public class Startup
  {
    private readonly ILogger _logger;
    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
      Configuration = configuration;
      _logger = logger;
      Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
    }

    public IConfiguration Configuration { get; }

    /// Implementation for sending back an empty HTTP response back to the client without causing redirects if request is unauthorized or denied
    static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode, Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
    context => {
        if (context.Request.Path.StartsWithSegments("/api/v1")) {
            context.Response.StatusCode = (int)statusCode;
            return Task.CompletedTask;
        }
        return existingRedirector(context);
    };

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(opt => {
          opt.UseCentralRoutePrefix(new RouteAttribute("api/v1"));
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddConnectionProvider(Configuration)
        .ConfigureSupervisor()
        .AddMiddleware()
        .AddCorsConfiguration()
        .ConfigureRepositories();

      services.AddIdentity<User, IdentityRole>(options =>{
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
      })
      .AddRoles<IdentityRole>()
      .AddUserManager<UserManager<User>>()
      .AddRoleManager<RoleManager<IdentityRole>>()
      .AddEntityFrameworkStores<MoviesPlaceContext>(); // Tell Identity which EF DbContext to use

      services.ConfigureApplicationCookie(options =>
      {
        options.Events.OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden, options.Events.OnRedirectToAccessDenied);
        options.Events.OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized, options.Events.OnRedirectToLogin);

        options.SlidingExpiration = true;
      });

      services.ConfigureJsonWebToken(Configuration);

      services.AddRouteAnalyzer();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
                          IHostingEnvironment env, 
                          IApplicationLifetime applicationLifetime,
                          UserManager<User> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IRouteAnalyzer routeAnalyzer,                          
                          MoviesPlaceContext context)
    {
      if (env.IsDevelopment())
      {        
        app.UseDeveloperExceptionPage();    
      }
      else
      {
        app.UseHsts();
      }

      app.ConfigureExceptionHandler(_logger);

      app.UseHttpsRedirection();

      app.UseAuthentication();

      app.SeedDatabase();

      app.UseMvc();

      applicationLifetime.ApplicationStarted.Register(() =>
      {
        var infos = routeAnalyzer.GetAllRouteInformations();
        Debug.WriteLine("======== ALL ROUTE INFORMATION ========");
        foreach (var info in infos)
        {
          Debug.WriteLine(info.ToString());
        }
        Debug.WriteLine("");
        Debug.WriteLine("");
      });
    }
  }
}
