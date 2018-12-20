//using log4net;
//using Microsoft.AspNet.SignalR;
//using Microsoft.Owin;
//using Owin;
//using Sghis.Core.Entity.Common;
//using Sghis.Core.Entity.Qms;
//using Sghis.QmsClient.Business;
//using Sghis.QmsClient.Data;
//using Sghis.QmsClient.UI.Controllers;
//using System;
//using System.Web.Cors;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Web.Routing;


//[assembly: OwinStartup(typeof(Sghis.QmsClient.UI.Startup))]
//namespace Sghis.QmsClient.UI
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
//            //enable cors origin requests
//            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

//            log4net.Config.XmlConfigurator.Configure();
//            var log = LogManager.GetLogger("QmsLogs");
//            log.Info("--Starting QMS API--");

//            var resolver = new DefaultDependencyResolver();
//            var dataManager = new QmsDataManager();

//            resolver.Register(typeof(IQmsDataManager), () => dataManager);
//            resolver.Register(typeof(IQueuingBusiness), () => new QueuingBusiness(dataManager));
//            resolver.Register(typeof(IAdminBusiness), () => new AdminBusiness(dataManager));
//            GlobalHost.DependencyResolver = resolver;
            
//            //Mvc
//            AreaRegistration.RegisterAllAreas();
//            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);

//            //webAPI
//            HttpConfiguration config = new HttpConfiguration();
//            WebApiConfig.Register(config);
//            app.UseWebApi(config);

//            app.MapSignalR();
//        }

//    }
//}
