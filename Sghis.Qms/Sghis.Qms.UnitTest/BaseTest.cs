using LightInject;
using Sghis.Core.Entity.Qms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Sghis.Qms.Data;
using Sghis.Qms.Business;

namespace Sghis.Qms.UnitTest
{
    public abstract class BaseTest {
        private ServiceContainer container;
        public ServiceContainer Container { get { return container; } }
        public BaseTest() {
            //log4net.Config.XmlConfigurator.Configure();
            container = new ServiceContainer();

            container.Register<QmsDbContext>();
            container.Register<IQmsDataManager, QmsDataManager>();
            container.Register<IQueuingBusiness, QueuingBusiness>(); 
        }
    }
}