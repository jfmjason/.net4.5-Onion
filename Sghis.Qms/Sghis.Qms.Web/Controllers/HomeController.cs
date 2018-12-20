using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Qms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Sghis.Qms.Web.Controllers
{

    public class HomeController : QmsBaseController
    {
        IQueuingBusiness business;

        public HomeController()
        {
            business = (IQueuingBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IQueuingBusiness));
        }
        public ActionResult Index()
        {
            var r = Request;
            ViewBag.Business = business.ToString();
            return View();
        }
        public ActionResult Display1()
        {
            var client = business.GetClientByIpAddress(GetUserIp());
            ViewBag.ZoneId = client.Zone.Id;
            ViewBag.ClientId = client.Id;
            return View();
        }
        public ActionResult Display2()
        {
            return View();
        }
        public ActionResult Display3()
        {
            return View();
        }

    }



}
