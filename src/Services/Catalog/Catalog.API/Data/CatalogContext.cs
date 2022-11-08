using Catalog.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration, ILogger<CatalogContext> logger)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDbSettings:DatabaseName"));
            logger.LogInformation(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            Console.WriteLine(configuration.GetValue<string>("MongoDbSettings:ConnectionString"));
            Courses = database.GetCollection<Course>(configuration.GetValue<string>("MongoDbSettings:CollectionName"));
            CatalogContextSeeder.SeedData(Courses);
        }
        public IMongoCollection<Course> Courses { get; }
    }
}
