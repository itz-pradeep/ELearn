using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeeder
    {
        public static void SeedData(IMongoCollection<Course> courses)
        {
            bool existsCourses = courses.Find(p => true).Any();

            if (!existsCourses)
            {
                var data = GetFakeData();

                courses.InsertManyAsync(data);
            }
        }

        private static IEnumerable<Course> GetFakeData()
        {
            return new List<Course>()
            {
                new Course()
                {
                    Id = (new Guid()).ToString(),
                    Name = "DSA",
                    Description = "Practical with C# DSA",
                    CreatedDate = DateTimeOffset.Now,
                    Hours = 2,
                    Minutes = 2,
                    LaunchUrl = "https://localhost:2022",
                    Technology = "C#,Dotnet",
                    IsActive = true
                },
                new Course()
                {
                    Id = (new Guid()).ToString(),
                    Name = "DSA",
                    Description = "Microservces with C# .NET",
                    CreatedDate = DateTimeOffset.Now,
                    Hours = 5,
                    Minutes = 3,
                    LaunchUrl = "https://localhost:2024",
                    Technology = "C#,Dotnet",
                    IsActive = true
                }
            };
        }
    }
}
