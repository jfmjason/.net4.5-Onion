using log4net;
using System.Configuration;
using System.Web.Mvc;


namespace Sghis.QmsClient.UI
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
                var log = LogManager.GetLogger("MvcRequest");

                log.Info(string.Format("Request {0}: {1}/{2} " + System.Environment.NewLine + "Parameter: {2}"
                    , filterContext.RequestContext.HttpContext.Request.HttpMethod
                    , controllerName, actionName, filterContext.RequestContext.HttpContext.Request.QueryString));
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
