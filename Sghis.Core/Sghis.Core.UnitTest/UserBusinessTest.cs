using Sghis.Core.Entity.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.UnitTest.Business
{

    [TestClass]
    public class UserBusinessTest : BaseTest
    {

        IUserBusiness business;

        [TestInitialize]
        public void Setup()
        {
            business = Container.GetInstance<IUserBusiness>();
        }

        [TestMethod]
        public void SanityTest()
        {
            Assert.IsInstanceOfType(business, typeof(IUserBusiness));
        }

        [TestMethod]
        public void Can_Get_User()
        {
            var userId = 1;
            var result = business.GetUserById(userId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Can_Delete_User()
        {
            var userId = 5;
            var result = business.DeleteUser(userId, "alfadz");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Can_Get_Users_By_Sql()
        {
            var results = business.SqlGetAllUsers();
            Assert.AreNotEqual(0, results.Count);
        }
    }
}