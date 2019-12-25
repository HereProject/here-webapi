using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using here_webapi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using here_webapi.Contracts.V1.Requests.Identity;
using here_webapi.Contracts.V1;
using here_webapi.Contracts.V1.Requests.Identity.Role;
using Here_Web_All.Data;

namespace here_webapi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpPost(ApiRoute.Roles.CreateRole)]
        public async Task<IActionResult> PostAppRole([FromBody] GenerateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new VOneResponse() { Success = false, Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
            }

            if (_context.Roles.Any(x => x.Name == request.RoleName))
            {
                return BadRequest(new VOneResponse() { Success = false, Errors = new string[] { "Rol adı zaten tanımlı." } });
            }

            AppRole newRole = new AppRole(request.RoleName);
            var result = await _roleManager.CreateAsync(newRole);

            if (!result.Succeeded)
            {
                return BadRequest(new VOneResponse() { Success = false, Errors = result.Errors.Select(x => x.Description) });
            }
            else
            {
                return StatusCode(201, newRole);
            }            
        }

        [HttpPost(ApiRoute.Roles.UserToRole)]
        public async Task<IActionResult> UserToRole([FromBody] UserToRoleRequest request)
        {
            if(!(await _context.Users.AnyAsync(x => x.Id == request.UserId)))
            {
                return BadRequest(
                    new VOneResponse() { Success = false, Errors = new string[] { "Kullanıcı bulunamadı!" } }
                    );
            }

            if (!(await _context.Roles.AnyAsync(x => x.Id == request.RoleId)))
            {
                return BadRequest(
                    new VOneResponse() { Success = false, Errors = new string[] { "Rol bulunamadı!" } }
                    );
            }

            if(await _context.UserRoles.AnyAsync(x => x.UserId == request.UserId && x.RoleId == request.RoleId))
            {
                if (!(await _context.Users.AnyAsync(x => x.Id == request.UserId)))
                {
                    return BadRequest(
                        new VOneResponse() { Success = false, Errors = new string[] { "Kullanıcı zaten bu role sahip!" } }
                        );
                }
            }

            _context.UserRoles.Add(new AppUserRole() { UserId = request.UserId, RoleId = request.RoleId });
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
    }
}
