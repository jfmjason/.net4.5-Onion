using LightInject;
using Newtonsoft.Json.Serialization;
using Sghis.Core.Business;
using Sghis.Core.Data;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;

namespace Sghis.Core.Api
{
    public class LightningMcQueen
    {
        private static ServiceContainer container;
        //I just love Flash to initialize the LightInject
        public static void Flash()
        {
            container = new ServiceContainer();
            container.RegisterApiControllers();
            //container.RegisterControllers();
            //register other services

            container.Register<SghisCoreDbContext>();
            container.Register<IDataManager, DataManager>();

            container.Register<IUserBusiness, UserBusiness>();
            container.Register<ISecurityBusiness, SecurityBusiness>();
            container.Register<AuthorizationServerProvider>();  

            container.RegisterControllers(typeof(MvcRouteConfig).Assembly);
            container.EnableWebApi(GlobalConfiguration.Configuration);
            //container.EnablePerWebRequestScope();
            container.EnableMvc();

        }

        public static ServiceContainer Container { get { return container; } }
    }
}
