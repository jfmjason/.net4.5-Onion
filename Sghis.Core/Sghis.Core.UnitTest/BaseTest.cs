using LightInject;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Common;
using Sghis.Core.Business;
using Sghis.Core.Data;


namespace Sghis.Core.UnitTest
{
    public abstract class BaseTest {
        private ServiceContainer container;
        public ServiceContainer Container { get { return container; } }
        public BaseTest() {
            //log4net.Config.XmlConfigurator.Configure();
            container = new ServiceContainer();

            container.Register<SghisCoreDbContext>();
            container.Register<IDataManager, DataManager>();
            container.Register<IUserBusiness, UserBusiness>();
        }
    }
}