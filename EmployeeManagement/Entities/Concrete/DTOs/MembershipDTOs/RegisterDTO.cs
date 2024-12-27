using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.MembershipDTOs
{
    public record class RegisterDTO(
        string Email,
        string UserName,
        string FirstName,
        string LastName,
        string Password
        );
   
}
