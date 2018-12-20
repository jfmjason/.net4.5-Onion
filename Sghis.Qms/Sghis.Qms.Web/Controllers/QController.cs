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
    [RoutePrefix("api/q")]
    public partial class QController : QmsBaseApiController
    {
        IQueuingBusiness business;
        IAdminBusiness adminBusiness;
        IHubContext qmsContext;

        public QController()
        {
            business = (IQueuingBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IQueuingBusiness));
            adminBusiness = (IAdminBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IAdminBusiness));
            qmsContext = GlobalHost.ConnectionManager.GetHubContext<QmsHub>();
        }

        [HttpPost]
        [Route("token/createtest")]
        public IHttpActionResult CreateTest(FormDataCollection col)
        {
            var id = col["id"];
            if (id != "A123456")
                return Json("fail");
            var mrn = "101";
            var serviceType = 1;
            var token = business.GenerateToken(mrn, serviceType, GetUserIp(), User.Identity.Name);
            var vm = QmsMapper.MapTokenToVm(token);
            qmsContext.Clients.All.createAddToPendingResponse(vm);
            return Json(vm);
        }

        [HttpPost]
        [Route("token/create")]
        public IHttpActionResult Create(FormDataCollection col)
        {
            var mrn = col["mrn"];
            var serviceTypeId = int.Parse(col["serviceTypeId"]);
            var token = business.GenerateToken(mrn, serviceTypeId, GetUserIp(), User.Identity.Name);
            var vm = QmsMapper.MapTokenToVm(token);
            return Json(vm);
        }

        [HttpPost]
        [Route("token/callbyclientid")]
        public IHttpActionResult CallByClientId(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var clientId = int.Parse(col["clientId"]);
            var token = business.UpdateToken(id, TokenStatus.Serving, User.Identity.Name, null, clientId);
            var vm = QmsMapper.MapTokenToVm(token);

            var zoneClients = business.GetClientWithinZoneByClientId(clientId);
            //remove from pending
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .callRemoveFromPendingResponse(vm);
            //add to new serving
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .callAddToServingResponse(vm);

            return Json("Call Token");
        }

        [HttpPost]
        [Route("token/call")]
        [ApiActionLogger]
        public IHttpActionResult Call(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var token = business.UpdateTokenByIp(id, TokenStatus.Serving, User.Identity.Name, null, GetUserIp());
            var vm = QmsMapper.MapTokenToVm(token);

            var zoneClients = business.GetClientWithinZoneByClientId(token.ClientId.Value);
            //remove from pending
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .callRemoveFromPendingResponse(vm);
            //add to new serving
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .callAddToServingResponse(vm);

            //refresh grid
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);

            return Json("Call Token");
        }

        [HttpPost]
        [Route("token/delay")]
        public IHttpActionResult Delay(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var token = business.UpdateTokenByIp(id, TokenStatus.Delayed, User.Identity.Name, null, GetUserIp());
            var vm = QmsMapper.MapTokenToVm(token);

            var zoneClients = business.GetClientWithinZoneByClientId(token.ClientId.Value);
            //add to new serving
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .delayAddToPendingResponse(vm);
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .delayRemoveFromServingResponse(vm);

            //refresh grid
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);

            return Json("Done Token");
        }

        [HttpPost]
        [Route("token/donebyclientid")]
        public IHttpActionResult DoneByClientId(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var clientId = int.Parse(col["clientId"]);
            var token = business.UpdateToken(id, TokenStatus.Done, User.Identity.Name, null, clientId);
            var vm = QmsMapper.MapTokenToVm(token);

            var zoneClients = business.GetClientWithinZoneByClientId(clientId);
            //add to new serving
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .doneResponse(vm);

            //refresh grid
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);
            return Json("Done Token");
        }

        [HttpPost]
        [Route("token/done")]
        public IHttpActionResult Done(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var token = business.UpdateTokenByIp(id, TokenStatus.Done, User.Identity.Name, null, GetUserIp());
            var vm = QmsMapper.MapTokenToVm(token);

            var zoneClients = business.GetClientWithinZoneByClientId(token.ClientId.Value);
            //add to new serving
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .doneResponse(vm);

            //refresh grid
            qmsContext.Clients.Clients(zoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);
            return Json("Done Token");
        }

        [HttpPost]
        [Route("token/transfer")]
        public IHttpActionResult Transfer(FormDataCollection col)
        {
            var id = int.Parse(col["id"]);
            var zoneId = int.Parse(col["zoneId"]);
            var token = business.UpdateToken(id, TokenStatus.Transfered, User.Identity.Name, zoneId);
            var vm = QmsMapper.MapTokenToVm(token);
            var delZoneClients = business.GetClientWithinZoneById(int.Parse(vm.OldZoneId));
            var addZoneClients = business.GetClientWithinZoneById(zoneId);

            //remove from old ZoneClients
            qmsContext.Clients.Clients(delZoneClients.Select(c => c.ConnectionId).ToList())
                .transferRemoveFromServingResponse(vm);
            qmsContext.Clients.Clients(delZoneClients.Select(c => c.ConnectionId).ToList())
               .transferRemoveFromPendingResponse(vm);
            //add to new ZoneClients
            qmsContext.Clients.Clients(addZoneClients.Select(c => c.ConnectionId).ToList())
                .transferAddToPendingResponse(vm);

            //refresh grid
            qmsContext.Clients.Clients(delZoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);
            qmsContext.Clients.Clients(addZoneClients.Select(c => c.ConnectionId).ToList())
                .refreshGridResponse(vm);
            return Json("Transfer Token");
        }

        [Route("token/gettokens")]
        public IHttpActionResult GetTokens(string status = null)
        {
            var tokens = business.GetTokens(GetUserIp(), status);
            var vms = QmsMapper.MapTokenToVm(tokens);
            return Json(vms);
        }

        [HttpPost]
        [Route("token/gettokensdt")]
        public IHttpActionResult GetTokensDt(DtTokenRequest dtr)
        {
            var tokens = business.GetTokensByIpAddress(out int total, dtr.Start, dtr.Length, GetUserIp(), dtr.Status);
            var vms = QmsMapper.MapTokenToDtContent(tokens);
            return Json(new { draw = dtr.Draw, recordsFiltered = total, recordsTotal = total, data = vms });
        }

        [Route("token/getcurrentserving")]
        public IHttpActionResult GetTokenCurrrentlyServing(string ipAddress)
        {
            var token = business.GetTokenCurrrentlyServing(ipAddress);
            if(token== null)
                return Json("Not serving any token.");
            var vm = QmsMapper.MapTokenToVm(token);
            return Json(vm);
        }
    }
}
