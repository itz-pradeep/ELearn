using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<UserRoles> Roles { get; set; }
        public string MyProperty { get; set; }
    }

    public class UserRoles
    {
        public string Role { get; set; }
    }
}
