using Business.Abstract;
using Business.Messages;
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
        private readonly IUserService _userService;  
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO, CancellationToken cancellationToken)
        {
            var result =await _userService.Register(registerDTO);
            if (result.Success)
            {
                return Ok(AuthMessage.RegisterSuccessful);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO, CancellationToken cancellationToken)
        {
          var userPassword= await _userService.ChangePassword(changePasswordDTO);

            if (userPassword.Success)
                return Ok(AuthMessage.PasswordChangeSuccessful);
                 
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken)
        {
         var token=  await _userService.EmailPasswordToken(email);

            if (token.Success)
                return Ok(token);

            return BadRequest();    
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmailPassword(ChangePasswordTokenDTO changePasswordTokenDTO, CancellationToken cancellationToken)
        {
           var userPassword= await _userService.ChangeEmailPassword(changePasswordTokenDTO);

            if (userPassword.Success)
                return Ok(AuthMessage.PasswordChangeSuccessful);

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var result = await _userService.VerifyUser(loginDTO, cancellationToken);
            if (result.Success)
                return Ok(result);

            return BadRequest();
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

            return Ok(new { Token = "Token" });

        }
    }
}

