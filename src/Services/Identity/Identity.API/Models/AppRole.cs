using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Identity.API.Models
{
    [CollectionName("Roles")]
    public class AppRole : MongoIdentityRole<Guid>
    {
    }
}
