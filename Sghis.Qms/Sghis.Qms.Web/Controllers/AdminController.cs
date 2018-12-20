using Microsoft.AspNet.SignalR;
using Sghis.Core.Entity.Qms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sghis.Qms.Web.Controllers
{
    public class AdminController : QmsBaseController
    {
        IAdminBusiness business;
        IQueuingBusiness queuingBusiness;
        public AdminController()
        {
            business = (IAdminBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IAdminBusiness));
            queuingBusiness = (IQueuingBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IQueuingBusiness));
        }

        public ActionResult Index()
        {
            return View();
        }
        //Zone/Location/Group/Department
        public JsonResult GetZones()
        {
            int total = 0;
            var zones = business.GetZones(out total, 0, 10);
            var vms = QmsMapper.MapZoneToVm(zones);
            return Json(new { draw = 10, recordsFiltered = total, recordsTotal = total, data = vms }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetZonesDt()
        {
            int total = 0;
            var zones = business.GetZones(out total, 0, 10);
            var vms = QmsMapper.MapZoneToVm(zones);
            return Json(new { draw = 10, recordsFiltered = total, recordsTotal = total, data = vms }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientDt()
        {
            int total = 0;
            var zones = business.GetZones(out total, 0, 10);
            var vms = QmsMapper.MapZoneToVm(zones);
            return Json(new { draw = 10, recordsFiltered = total, recordsTotal = total, data = vms }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientByIpAddress(string ipAddress)
        {
            var client = business.GetClientByIpAddress(ipAddress);
            var vm = QmsMapper.MapClientToVm(client);
            vm.ApiVersion = GetType().Assembly.GetName().Version.ToString().Remove(5);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientById(int id)
        {
            var client = business.GetClientById(id);
            var vm = QmsMapper.MapClientToVm(client);
            vm.ApiVersion = GetType().Assembly.GetName().Version.ToString().Remove(5);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Token(int id)
        {
            var token = queuingBusiness.GetToken(id);
            var vm = QmsMapper.MapTokenToVmWithLogs(token);
            return View(vm);
        }
    }


}
