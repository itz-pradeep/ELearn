using Identity.API.Entities;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Helpers
{
    public class UsersSeeder
    {

        public static void InsertDemoRoles(IMongoCollection<AppRole> appRoles, RoleManager<AppRole> roleManager)
        {
            bool existsRoles = appRoles.Find(p => true).Any();
            if (!existsRoles)
            {
                var roles = new List<AppRole>
                {
                    new AppRole
                    {
                        Name = "NORMAL_USER"
                    },
                    new AppRole
                    {
                        Name = "ADMIN"
                    }
                };

                foreach(var role in roles)
                {
                    var result = roleManager.CreateAsync(role).Result;

                    if (result.Succeeded) Console.WriteLine("Role added");
                }

            }
        }

        public static void InsertDemoUsers(IMongoCollection<AppUser> appUsers,RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            bool existsUsers = appUsers.Find(p => true).Any();

            if (!existsUsers)
            {
                var adminUsers = GetFakeAdminsData();
                var adminRole = roleManager.FindByNameAsync("ADMIN").Result;
                var userRole = roleManager.FindByNameAsync("NORMAL_USER").Result;
                foreach (var usr in adminUsers)
                {
                    var result = userManager.CreateAsync(usr).Result;
                    if (result.Succeeded)
                        userManager.AddToRoleAsync(usr, adminRole.Name);
                }

                var normalUsers = GetFakeUsersData();

                foreach (var usr in normalUsers)
                {
                    var result = userManager.CreateAsync(usr).Result;
                    if (result.Succeeded)
                        userManager.AddToRoleAsync(usr, userRole.Name).ConfigureAwait(false); ;
                }
            }
        }
      
        private static List<AppUser> GetFakeUsersData()
        {
            return new List<AppUser>() { 
                new AppUser
                {
                    UserName = "its-pradeep-user",
                    Email = "pradeepasuser@gmail.com"
                }
            };
        }

        private static List<AppUser> GetFakeAdminsData()
        {
            return new List<AppUser>() {
                new AppUser
                {
                    UserName = "its-pradeep-admin",
                    Email = "pradeepasadmin@gmail.com"
                }
            };
        }
    }
}
