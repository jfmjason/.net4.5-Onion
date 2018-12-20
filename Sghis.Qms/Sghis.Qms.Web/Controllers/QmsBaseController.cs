using Microsoft.AspNet.SignalR;
using Sghis.Core.Entity.Qms;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sghis.Qms.Web.Controllers
{

    [MvcExceptionHandler]
    [MvcActionLogger]
    [AllowCrossSite]
    public class QmsBaseController : Controller
    {
        public QmsBaseController()
        {

        }

        protected string GetUserIp()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }

    [ApiExceptionHandler]
    [ApiActionLogger]
    public class QmsBaseApiController : ApiController
    {
        public QmsBaseApiController()
        {
           
        }

        protected string GetUserIp()
        {
            return Request.GetOwinContext().Request.RemoteIpAddress;
        }
    }

}
