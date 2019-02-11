﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalErrorHandling.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoviesPlaceAPI.Configurations;

namespace MoviesPlaceAPI
{
  public class Startup
  {
    private readonly ILogger _logger;
    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
      Configuration = configuration;
      _logger = logger;
    }

    public IConfiguration Configuration { get; }

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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
      app.UseMvc();
    }
  }
}
