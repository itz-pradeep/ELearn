using Identity.API.Dtos;
using Identity.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public OperationsController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto request)
        {
            var appRole = new AppRole
            {
                Name = request.Role
            };

            var result = await _roleManager.CreateAsync(appRole);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
