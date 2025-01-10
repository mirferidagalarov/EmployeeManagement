using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Security
{
    public interface ITokenHelper
    {
        Token CreateToken(AppUser user);
    }
}
