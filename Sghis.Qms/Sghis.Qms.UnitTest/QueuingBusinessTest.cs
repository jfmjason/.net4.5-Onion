using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Qms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Qms.UnitTest.Business
{
    [TestClass]
    public class QueuingBusinessTest : BaseTest
    {

        IQueuingBusiness business;

        [TestInitialize]
        public void Setup()
        {
            business = Container.GetInstance<IQueuingBusiness>();
        }

        [TestMethod]
        public void SanityTest()
        {
            Assert.IsInstanceOfType(business, typeof(IQueuingBusiness));
        }

        [TestMethod]
        public void Can_Generate_Token()
        {
            var mrn = "101";
            var ipAddress = "::1";
            var serviceType = 1;
            var createdBy = "Seed";
            var token = business.GenerateToken(mrn, serviceType, ipAddress, createdBy);
            Assert.IsNotNull(token);
        }


        [TestMethod]
        public void Can_Get_Token()
        {
            var tokenId = 1;
            var token = business.GetToken(tokenId);
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void Can_Get_Tokens()
        {
            var ipAddress = "::1";

            var queues = business.GetTokens(ipAddress, null);
            Assert.IsNotNull(queues);
        }


        [TestMethod]
        public void Can_Call_Token()
        {
            var tokenId = 1;
            var updateStatus = TokenStatus.Serving;
            var ipAddress = "::1";
            var token = business.UpdateTokenByIp(tokenId, updateStatus, "Test", null, ipAddress);
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void Can_Transfer_Token()
        {
            var tokenId = 1;
            var updateStatus = TokenStatus.Transfered;
            var newZoneId = 2;
            var token = business.UpdateToken(tokenId, updateStatus, "Test", newZoneId);
            var vm = QmsMapper.MapTokenToVm(token);
            Assert.Equals(newZoneId, vm.NewZoneId);
        }

        [TestMethod]
        public void Can_Delayed_Token()
        {
            var tokenId = 1;
            var updateStatus = TokenStatus.Delayed;
            var token = business.UpdateToken(tokenId, updateStatus, "Test");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void Can_Done_Token()
        {
            var tokenId = 1;
            var updateStatus = TokenStatus.Done;
            var token = business.UpdateToken(tokenId, updateStatus, "Test");
            Assert.IsNotNull(token);
        }


        [TestMethod]
        public void Can_Decrypt_Text()
        {
            var decrypted = CryptoHelper.Decrypt("fW2ZGWB9aRfPUPpRe4qIf5CE4Kspp3tmjx7aXXbqxM3YXVpEvsMqOFDtY8/mp7iDX98QlK8kniGcwUoPwZy6og+DrtKjOOteRNtwW1yBtLup6Wrcqs6ySXsCmtDNuvzG5T7ltX6o5BZ2xejwjyshIkvundJF7lNI4l/PiH+vvISXfljIGtmcUw==", true);
            Assert.IsNotNull(decrypted);
        }


        [TestMethod]
        public void Can_Get_ClientWithinZone_ById()
        {
            var zoneId = 1;
            var clients = business.GetClientWithinZoneById(zoneId);
            Assert.IsNotNull(clients);
        }

        [TestMethod]
        public void Can_Get_ClientWithinZone_ByClientId()
        {
            var clientId = 2;
            var clients = business.GetClientWithinZoneByClientId(clientId);
            Assert.IsNotNull(clients);
        }

        [TestMethod]
        public void Can_Get_Current_Serving_ByIp()
        {
            var ipAddress = "130.1.8.232";
            var token = business.GetTokenCurrrentlyServing(ipAddress);
            Assert.IsNotNull(token);
        }
    }
}