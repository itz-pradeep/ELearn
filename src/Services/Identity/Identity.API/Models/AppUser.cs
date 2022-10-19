using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Identity.API.Models
{
    [CollectionName("Users")]
    public class AppUser : MongoIdentityUser<Guid>
    {
    }
}
