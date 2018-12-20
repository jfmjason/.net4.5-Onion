using Microsoft.AspNet.SignalR;
using Sghis.Core.Entity.Qms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace Sghis.Qms.Web.Controllers
{
    public partial class QController : QmsBaseApiController
    {

        [Route("admin/getclient")]
        public IHttpActionResult GetClientByIpAddress(string ipAddress)
        {
            var client = business.GetClientByIpAddress(ipAddress);
            var vm = QmsMapper.MapClientToVm(client);
            vm.ApiVersion = GetType().Assembly.GetName().Version.ToString().Remove(5);
            return Json(vm);
        }

        [Route("admin/getzone")]
        public IHttpActionResult GetZonesByIpAddress(string ipAddress)
        {
            var zones = adminBusiness.GetZonesByIpAddress(ipAddress);
            var vm = QmsMapper.MapZoneToVm(zones);
            return Json(vm);
        }

        [Route("admin/getzones")]
        public IHttpActionResult GetZones()
        {
            var zones = adminBusiness.GetZones();
            var vm = QmsMapper.MapZoneToVm(zones);
            return Json(vm);
        }
    }
}
