using Core.Results.Abstract;
using Entities.Concrete.DTOs.MembershipDTOs;
using Entities.Concrete.Membership;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> Register(RegisterDTO user);
        Task<IResult> ChangePassword(ChangePasswordDTO dto);
        Task<IResult> ChangeEmailPassword(ChangePasswordTokenDTO dto);
        Task<IResult> EmailPasswordToken(string email);   
        Task<IResult> VerifyUser(LoginDTO loginDTO,CancellationToken cancellationToken);
        Task<IResult> RefreshTokenLogin(string refreshToken);

    }
}
