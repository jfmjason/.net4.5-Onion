using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Sghis.Core.Business;
using Sghis.Core.Entity.Common;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Sghis.Core.Api.Startup))]
namespace Sghis.Core.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //register components to lightinject
            LightningMcQueen.Flash();

            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //enable cors origin requests
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var security = LightningMcQueen.Container.GetInstance<ISecurityBusiness>();

            var authProvider = new AuthorizationServerProvider(security);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = authProvider
                //, RefreshTokenProvider = new RefreshTokenProvider()
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //webAPI
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            //Mvc
            AreaRegistration.RegisterAllAreas();          
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
        }

    }

}