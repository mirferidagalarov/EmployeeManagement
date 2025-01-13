using Entities.Concrete.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDAL
    {
        Task<AppUser> FindRefreshToken(string refreshToken);
    }
}
