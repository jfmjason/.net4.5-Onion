using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.DirectoryServices;
using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Base;

namespace Sghis.Core.Business
{

    public class SecurityBusiness : BaseBusiness, ISecurityBusiness
    {

        public SecurityBusiness(IDataManager mgr)
            : base(mgr) { }

        public Client GetClientById(int id)
        {
            return Data.Client.GetById(id);
        }

        public async Task<User> LoginUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new Exception("username or password cannot be empty");

            var user = await Data.User.GetAll().Where(c => c.UserName == userName && c.Password == password).ToListAsync();
            if (user == null)
                throw new Exception("user not found");
            return user[0];
        }
    }
}