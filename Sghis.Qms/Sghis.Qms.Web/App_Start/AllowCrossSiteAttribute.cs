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
using System.Web.Cors;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sghis.Qms.Web
{
    public class AllowCrossSiteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = filterContext.RequestContext.HttpContext;
            var origin = ctx.Request.Headers["Origin"];
            var allowOrigin = !string.IsNullOrWhiteSpace(origin) ? origin : "*";
            ctx.Response.AddHeader("Access-Control-Allow-Origin", allowOrigin);
            ctx.Response.AddHeader("Access-Control-Allow-Headers", "*");
            ctx.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            base.OnActionExecuting(filterContext);
        }
    }
}
