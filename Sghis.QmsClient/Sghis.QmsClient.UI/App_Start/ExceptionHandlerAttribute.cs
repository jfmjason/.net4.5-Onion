using log4net;
using Sghis.QmsClient.UI.Controllers;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace Sghis.QmsClient.UI
{
    public class MvcExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled) return;

            Exception ex = context.Exception;
            context.ExceptionHandled = true;
            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();

            var log = LogManager.GetLogger(typeof(MvcExceptionHandlerAttribute).Name);
            log.Error(string.Format("Error while executing {0}: {1}/{2} " + System.Environment.NewLine
                + "Parameter: {3}", context.RequestContext.HttpContext.Request.HttpMethod,
                controllerName, actionName, context.HttpContext.Request.Form));

            log.Error(string.Format("Stacktrace:  {0}", context.Exception));
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                // if request was an Ajax request, respond with json with Error field
                //var jsonResult = new AjaxErrorController { ControllerContext = context }.GetJsonError(context.Exception);
                //jsonResult.ExecuteResult(context);
            }
            else
            {
                // if not an ajax request, continue with logic implemented by MVC -> html error page
                base.OnException(context);
            }
        }
    }

}
