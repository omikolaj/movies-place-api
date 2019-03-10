using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MoviesDomain.Models;
using MoviesPlaceAPI.Auth;
using MoviesPlaceAPI.Auth.JwtTokenProvider;

namespace MoviesPlaceAPI.Configurations
{
  public static class JSONWebTokenConfiguration
  {
    public static IServiceCollection ConfigureJsonWebToken(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<IJwtFactory, JwtFactory>();

      string privateKey = "PrivateKeyJSONWebTokenConfigurationPrivateKeyJSONWebTokenConfigurationPrivateKeyJSONWebTokenConfigurationPrivateKeyJSONWebTokenConfiguration";
      SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));

      var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(signingKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        RequireSignedTokens = true,
        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;        
      });

      // api user claim policy
      services.AddAuthorization(options =>
      {
        options.AddPolicy("AdminPolicy", policy => {
          policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Role, "Admin");
          // Runs only against the identity created by the "Bearer" handler
          policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        });
      });

      return services;
    }
  }
}