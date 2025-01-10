using Business.Abstract;
using Business.Security;
using Core.Results.Abstract;
using Core.Results.Concrete;
using Entities.Concrete.DTOs.MembershipDTOs;
using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{
    public class UsersManager : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ITokenHelper _tokenHelper;
        public UsersManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, ITokenHelper tokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResult> Register(RegisterDTO registerDTO)
        {
            AppUser user = new()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();

                foreach (var error in errorMessages)
                {
                    return new ErrorResult(error);
                }

                return new ErrorResult(string.Join("; ", errorMessages));
            }

            return new SuccessResult();
        }

        public IResult DeleteUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> RefreshTokenLogin(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public IResult UpdatePassword(AppUser dto)
        {
            throw new NotImplementedException();
        }

        public IResult UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public async Task<IResult> VerifyUser(LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Email == loginDTO.UserNameOrEmail || p.UserName == loginDTO.UserNameOrEmail, cancellationToken);
            if (appUser == null)
                return new ErrorResult("User NotFound");

            bool result = await _userManager.CheckPasswordAsync(appUser, loginDTO.Password);

            if (!result)
                return new ErrorResult("Password Incorrect");

            //Token token= TokenHandler.CreateToken(appUser);
           var token= _tokenHelper.CreateToken(appUser);
            return new SuccessDataResult<Token>(token);
        }
    }
}
