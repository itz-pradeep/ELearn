using Identity.API.Dtos;
using Identity.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;

        public IdentityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterUserDto request)
        {
            AppUser user = new AppUser
            {
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "NORMAL_USER");

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userManager.Users.ToList();

            return Ok(result);
        }


    }
}
