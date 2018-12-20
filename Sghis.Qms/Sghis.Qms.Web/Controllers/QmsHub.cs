using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNet.SignalR.Infrastructure;
using Sghis.Core.Entity.Qms;
using Sghis.Core.Entity.Base;
using Newtonsoft.Json;
using System.Configuration;

namespace Sghis.Qms.Web
{
    [Authorize]
    public class QmsHub : Hub
    {
        public override Task OnConnected()
        {
            var ip = Context.Request.Environment["server.RemoteIpAddress"].ToString();

            var currentuser = (CustomPrincipal)Context.User;
            if (Context.User.Identity.IsAuthenticated)
            {
                var isIpAddressValid = true;
                var restrictIpAddress = bool.Parse(ConfigurationManager.AppSettings["RestrictIpAddress"].ToString());
                if (restrictIpAddress && (ip != currentuser.IpAddress))
                    isIpAddressValid = false;

                if (isIpAddressValid)
                {
                    QmsBroadcast.Instance.UserConnected(Context.ConnectionId, ip);
                }
            }
            else
            {
                Clients.Client(Context.ConnectionId).invalidUserResponse("Cannot connect to QMS. Device is not registered.");
            }
            return base.OnConnected();

        }

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    QmsBroadcast.Instance.UserDisconnected(Context.ConnectionId);
        //    return base.OnDisconnected(stopCalled);
        //}

        public override Task OnReconnected()
        {

            return base.OnReconnected();
        }
    }


    public class QmsBroadcast
    {
        private readonly static Lazy<QmsBroadcast> _instance = new Lazy<QmsBroadcast>(() =>
                  new QmsBroadcast(GlobalHost.ConnectionManager.GetHubContext<QmsHub>()
                      , (IQueuingBusiness)GlobalHost.DependencyResolver.Resolve(typeof(IQueuingBusiness))));

        public static QmsBroadcast Instance {
            get {
                return _instance.Value;
            }
        }
        private IQueuingBusiness business;
        private IHubContext context;
        private QmsBroadcast(IHubContext ctx, IQueuingBusiness business)
        {
            context = ctx;
            this.business = business;
        }

        public void UserConnected(string connectionId, string ip)
        {
            var client = new Client()
            {
                IpAddress = ip,
                ActionBy = "OnConnected",
            };
            business.ClientConnected(connectionId, client);
            context.Clients.Client(connectionId).userConnectedResponse("Successfully connected to QMS");
        }

        public void UserDisconnected(string connectionId)
        {
            business.DeleteConnection(connectionId);
        }
    }

}