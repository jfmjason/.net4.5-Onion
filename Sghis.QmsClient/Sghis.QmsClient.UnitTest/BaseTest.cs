using LightInject;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.QmsClient.Infra.Business;
using Sghis.QmsClient.Infra.Data;

namespace Sghis.QmsClient.UnitTest
{
    public abstract class BaseTest {
        private ServiceContainer container;

        public ServiceContainer Container { get { return container; } }
        public BaseTest() {
            container = new ServiceContainer();
            container.Register<QmsClientDbContext>();
            container.Register<IQmsClientDataManager, QmsClientDataManager>();
            container.Register<IQmsClientUserBusiness, QmsClientUserBusiness>();
            container.Register<IQmsClientMasterFileBusiness, QmsClientMasterFileBusiness>(); 
        }
    }
}