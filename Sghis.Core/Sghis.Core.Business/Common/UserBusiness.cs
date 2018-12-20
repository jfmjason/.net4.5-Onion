using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Base;

namespace Sghis.Core.Business {



    public class UserBusiness : BaseBusiness, IUserBusiness {

        public UserBusiness(IDataManager mgr)
            : base(mgr) {
        }

        public bool DeleteUser(int id, string actionBy) {
            var user = Data.User.GetById(id);
            if (user != null) {
                user.Deleted = false;
                user.ModifiedAt = DateTime.Now;
                user.ModifiedBy = actionBy;
                Data.User.Update(user);

                Data.User.Commit();
            }
            return true;
        }

        public User GetUserById(int id) {
            return Data.User.GetById(id);
        }

        public User GetUserByUserName(string userName) {
            return Data.User.GetAll().Where(c => c.UserName.ToLower() == userName.ToLower()).FirstOrDefault();
        }

        public List<User> GetAllUsers() {
            return Data.User.GetAll().ToList();
        }

        public List<UserSp> SqlGetAllUsers()
        {
            var query = "SELECT [Id] ,[UserName] ,[Email] ,[Department] ,[FullName] FROM [Common].[User]";
            return Data.UserSp.GetAll(query);
        }
    }
}