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
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sghis.Qms.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
