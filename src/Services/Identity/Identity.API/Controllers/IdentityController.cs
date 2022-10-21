using Identity.API.Dtos;
using Identity.API.Entities;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/v1.0/lms")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public IdentityController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDto request)
        {
            AppUser user = new AppUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user,request.Password);

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

        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] LoginDto request)
        {
            var appUser = await _userManager.FindByEmailAsync(request.Email);
            if(appUser == null)
            {
                return NotFound();
            }

            var result = await _signInManager.PasswordSignInAsync(appUser,request.Password,false,false);
            if(!result.Succeeded)
            {
                return Unauthorized();
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(appUser).ConfigureAwait(false);
            return Ok(new AppUserDto
            {
                Id = appUser.Id.ToString(),
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Token = _tokenService.CreateToken(appUser),
                Roles = userRoles
            });
        }


        //[Authorize(Roles = "ADMIN")]
        //Test
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userManager.Users.ToList();

            return Ok(result);
        }


    }
}
