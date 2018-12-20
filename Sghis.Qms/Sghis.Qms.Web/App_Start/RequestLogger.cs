using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Qms;
using Sghis.Qms.Business;
using Sghis.Qms.Data;
using Sghis.Qms.Web.Controllers;
using System;
using System.Configuration;
using System.Web.Cors;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;


namespace Sghis.Qms.Web
{
    public class MvcActionLoggerAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var logRequest = bool.Parse(ConfigurationManager.AppSettings["LogRequest"].ToString());
            if (logRequest)
            {
                var controllerName = filterContext.RequestContext.RouteData.Values["Controller"];
                var actionName = filterContext.RequestContext.RouteData.Values["Action"];
                log4net.Config.XmlConfigurator.Configure();
                var log = LogManager.GetLogger("MVC");

                log.Info(string.Format("{0}-->{1}:{2}/{3} " + System.Environment.NewLine + "Parameter: {4}"
                    , filterContext.RequestContext.HttpContext.Request.UserHostAddress
                    , filterContext.RequestContext.HttpContext.Request.HttpMethod
                    , controllerName, actionName, filterContext.RequestContext.HttpContext.Request.QueryString));
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class ApiActionLoggerAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var logRequest = bool.Parse(ConfigurationManager.AppSettings["LogRequest"].ToString());
            if (logRequest)
            {
                var url = context.Request.RequestUri.ToString();
                var ip = context.Request.Headers.Host;

                log4net.Config.XmlConfigurator.Configure();
                var log = LogManager.GetLogger("API");

                log.Info(string.Format("{0}-->{1}:{1} ", ip, context.Request.Method, url));
            }

            base.OnActionExecuting(context);
        }

    }
}
