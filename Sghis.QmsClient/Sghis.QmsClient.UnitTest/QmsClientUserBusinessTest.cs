using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sghis.Core.Entity.QmsClient.Interface;

namespace Sghis.QmsClient.UnitTest.Business
{
    [TestClass]
    public class QmsClientUserBusinessTest : BaseTest
    {

        IQmsClientUserBusiness business;

        [TestInitialize]
        public void Setup()
        {
            business = Container.GetInstance<IQmsClientUserBusiness>();
        }

        [TestMethod]
        public void SanityTest()
        {
            Assert.IsInstanceOfType(business, typeof(IQmsClientUserBusiness));
        }

        [TestMethod]
        public void Can_GetMenus()
        {
            var userid = 11156;
            var moduleid = 324;

            var menus = business.GetMenuAccess(userid, moduleid);
            Assert.IsNotNull(menus);
        }

    }
}