using Identity.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Data
{
    public interface IIdentityContext
    {
        IMongoCollection<AppUser> AppUser { get; }
        IMongoCollection<AppRole> AppRole { get; }
    }
}
