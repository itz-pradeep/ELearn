using Identity.API.Entities;
using Identity.API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Extensions
{
    public static class SeedUsersExtension
    {
        public static IServiceCollection SeedUsers(this IServiceCollection services,IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var client = new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
                var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DatabaseName"));
                var usersCollection = database.GetCollection<AppUser>("Users");
                var rolesCollection = database.GetCollection<AppRole>("Roles");
                UsersSeeder.InsertDemoRoles(rolesCollection,roleManager);
                UsersSeeder.InsertDemoUsers(usersCollection,roleManager,userManager);
            }

            return services;
               
        }
    }
}
