using Identity.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
