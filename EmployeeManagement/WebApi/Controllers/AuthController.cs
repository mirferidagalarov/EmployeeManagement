using Entities.Concrete.DTOs.MembershipDTOs;
using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO, CancellationToken cancellationToken)
        {
            AppUser user = new()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(s => s.Description));


            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(changePasswordDTO.ID.ToString());
            if (appUser is null)
                return BadRequest(new { Message = "User not found" });

            IdentityResult result = await _userManager.ChangePasswordAsync(appUser, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(s => s.Description));

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByEmailAsync(email);

            if (appUser is null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            return Ok(new { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordTokenDTO changePasswordTokenDTO, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByEmailAsync(changePasswordTokenDTO.Email);

            if (appUser is null)
            {
                return BadRequest(new { Message = "User not found" });
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(appUser, changePasswordTokenDTO.Token, changePasswordTokenDTO.NewPassword);



            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Email == loginDTO.UserNameOrEmail || p.UserName == loginDTO.UserNameOrEmail, cancellationToken);

            if (appUser is null)
            {
                return BadRequest(new { Message = "User Not Found" });
            }

            bool result = await _userManager.CheckPasswordAsync(appUser, loginDTO.Password);

            if (!result)
                return BadRequest(new { Message = "Password incorrect" });

            return Ok(new { Token = "Token" });

        }

        [HttpPost]

        public async Task<IActionResult> LoginWithSignInManager(LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Email == loginDTO.UserNameOrEmail || p.UserName == loginDTO.UserNameOrEmail, cancellationToken);
            if (appUser is null)
            {
                return BadRequest(new { Message = "User Not Found" });
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, true);

            if (signInResult.IsLockedOut)
            {
                TimeSpan? timeSpan = appUser.LockoutEnd - DateTime.Now;

                if (timeSpan is not null)
                {
                    return StatusCode(500, $"You have been blocked for {timeSpan.Value.TotalSeconds} seconds due to entering your password incorrectly 3 times");
                }
                else
                {
                    return StatusCode(500, $"You have been blocked for 30 seconds due to entering your password incorrectly 3 times");
                }
            }

            if (signInResult.IsNotAllowed)
            {
                return StatusCode(500, "Your email address is not verified.");
            }

            if (!signInResult.Succeeded)
            {
                return StatusCode(500, "Password Incorrect");
            }

            return Ok(new {Token="Token"});

        }
    }
}

