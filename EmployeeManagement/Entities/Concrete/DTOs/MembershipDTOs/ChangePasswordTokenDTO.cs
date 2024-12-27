using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.MembershipDTOs
{
    public sealed record ChangePasswordTokenDTO(
        string Email,
        string NewPassword,
        string Token
        );
    
}
