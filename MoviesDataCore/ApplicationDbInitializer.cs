using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using MoviesDomain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MoviesDataCore
{
  public class ApplicationDbInitializer
  {
    public static async void SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
      //Create roles [SuperUser, Admin, User]
      string[] roles = new string[] { "SuperUser", "Admin", "User" };

      foreach(string role in roles)
      { 
        if(!roleManager.Roles.Any(r => r.Name == role))
        {
          var newRole = new IdentityRole {Name = role, NormalizedName = role.ToUpper()};          
          await roleManager.CreateAsync(newRole);
          if(role == "SuperUser"){
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "ViewPosts"));
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "CreatePosts"));
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "EditPosts"));
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "DeletePosts"));
          }
          else if(role == "Admin"){
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "ViewPosts"));
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "CreatePosts"));
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "EditPosts"));
          }
          else if(role == "User"){
            await roleManager.AddClaimAsync(newRole, new Claim("permission", "ViewPosts"));
          }
        }
      }

      User admin = new User
            {
                UserName = "oski",
                Email = "abc@xyz.com"
            };

      User superUser = new User
            {
                UserName = "alicja",
                Email = "abc@xyz.com",
            };

      User user = new User
            {
                UserName = "kaja",
                Email = "abc@xyz.com",
            };

      PasswordHasher<User> password = new PasswordHasher<User>();
      // Create users and assign to roles
      if(!userManager.Users.Any(u => u.UserName == admin.UserName))
      {
        string hashed = password.HashPassword(admin, "password");
        admin.PasswordHash = hashed;
        
        await userManager.CreateAsync(admin);
        await userManager.AddToRoleAsync(admin, "ADMIN");
      }

      if(!userManager.Users.Any(u => u.UserName == superUser.UserName))
      {
        string hashed = password.HashPassword(superUser, "password");
        superUser.PasswordHash = hashed;

        await userManager.CreateAsync(superUser);        
        await userManager.AddToRoleAsync(superUser, "SUPERUSER");
      }

      if(!userManager.Users.Any(u => u.UserName == user.UserName))
      {
        string hashed = password.HashPassword(user, "password");
        user.PasswordHash = hashed;  

        await userManager.CreateAsync(user);
        await userManager.AddToRoleAsync(user, "USER");
      }
    }
  }  
}