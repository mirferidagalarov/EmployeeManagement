using Core.Results.Abstract;
using Entities.Concrete.DTOs.MembershipDTOs;
using Entities.Concrete.Membership;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> Register(RegisterDTO user);
        IResult UpdateUser(AppUser user);
        IResult UpdatePassword(AppUser dto);
        IResult DeleteUser(AppUser user);
        Task<IResult> VerifyUser(LoginDTO loginDTO,CancellationToken cancellationToken);
        Task<IResult> RefreshTokenLogin(string refreshToken);

    }
}
