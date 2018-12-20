using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Common
{
    public interface ISecurityBusiness
    {
        Task<User> LoginUser(string userName, string password);
        Client GetClientById(int id);
    }


}
