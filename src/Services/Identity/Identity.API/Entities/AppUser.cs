using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Identity.API.Entities
{
    [CollectionName("Users")]
    public class AppUser : MongoIdentityUser<Guid>
    {
    }
}
