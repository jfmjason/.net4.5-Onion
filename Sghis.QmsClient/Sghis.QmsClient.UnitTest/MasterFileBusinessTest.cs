using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sghis.Core.Entity.QmsClient.Interface;

namespace Sghis.QmsClient.UnitTest.Business
{
    [TestClass]
    public class MasterFileBusinessTest : BaseTest
    {

        IQmsClientMasterFileBusiness business;

        [TestInitialize]
        public void Setup()
        {
            business = Container.GetInstance<IQmsClientMasterFileBusiness>();
        }

        [TestMethod]
        public void SanityTest()
        {
            Assert.IsInstanceOfType(business, typeof(IQmsClientMasterFileBusiness));
        }

        [TestMethod]
        public void Can_PatientByRegistrationNo()
        {
            var registrationNo = 1;

            var patient = business.GetPatientByRegistrationNo(registrationNo);
            Assert.IsNotNull(patient);
        }


        [TestMethod]
        public void Can_OrganizationDetailById()
        {
            var id = 1;

            var oraganizationDetails = business.GetOrganisationDetailById(id);
            Assert.IsNotNull(oraganizationDetails);
        }


    }
}