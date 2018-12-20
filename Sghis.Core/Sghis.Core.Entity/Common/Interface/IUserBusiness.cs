using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Common
{
    public interface IUserBusiness
    {
        User GetUserById(int id);
        User GetUserByUserName(string userName);
        List<User> GetAllUsers();
        bool DeleteUser(int id, string actionBy);

        List<UserSp> SqlGetAllUsers();
    }


}
