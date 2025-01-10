using DataAccess.DatabaseContext;
using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public sealed class UserRolesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        public UserRolesController(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int userId, int roleId, CancellationToken cancellationToken)
        {
            AppUserRole appUserRole = new()
            {
                UserId = userId,
                RoleId = roleId,
            };

            await _dataContext.UserRoles.AddAsync(appUserRole);

            await _dataContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> CreateDiff(int userId, string roleName, CancellationToken cancellationToken)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId.ToString());

            if(appUser is null)
            {
                return BadRequest(new { Message = "User Not Found" });
            }

            IdentityResult result = await _userManager.AddToRoleAsync(appUser, roleName);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(s => s.Description));
            }

            return NoContent();
        }

    }
}
