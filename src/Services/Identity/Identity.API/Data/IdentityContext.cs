using Identity.API.Data;
using Identity.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Identity.API.Data
{
    public class IdentityContext : IIdentityContext
    {
        public IdentityContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DatabaseName"));
            //Courses = database.GetCollection<AppUser>(configuration.GetValue<string>("Users"));
            //CatalogContextSeeder.SeedData(Courses);
        }
        public MongoDB.Driver.IMongoCollection<AppUser> AppUser { get; }
        public MongoDB.Driver.IMongoCollection<AppRole> AppRole { get; }
    }
}
