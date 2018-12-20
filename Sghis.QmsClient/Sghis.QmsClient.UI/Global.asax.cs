﻿#region Using
using log4net;
using Sghis.Core.Entity.Base;
using Sghis.QmsClient.UI.App_Start;
using System;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

#endregion

namespace Sghis.QmsClient.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            var log = LogManager.GetLogger(typeof(MvcApplication).Name);
            log.Info("--Starting HIS-LAB Application--");
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MvcFilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
      
            LightningMcQueen.Flash();
            //AuthConfig.RegisterAuth();
        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //check cookie or header
            string encryptedUser = "";
            var authCookie = Request.Cookies["__sghis"];
            if (authCookie != null)
            {
                encryptedUser = authCookie.Value;
            }
            else
            {
                var authHeader = Request.Headers["__sghis"];
                if (authHeader != null)
                {
                    encryptedUser = authHeader;
                }

            }

            if (!string.IsNullOrEmpty(encryptedUser))
            {
                var decrypted = CryptoHelper.Decrypt(encryptedUser, true);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                CustomPrincipalSerializedModel serializeModel =
                  serializer.Deserialize<CustomPrincipalSerializedModel>(decrypted);
                CustomPrincipal curentUser = new CustomPrincipal(serializeModel.FullName);

                var requestIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //every login token is strictly valid to one machine only.
                //prevent using of token in other machine/device by checking the IP saved in token vs the request IP
                var isIpAddressValid = true;
                var restrictIpAddress = bool.Parse(ConfigurationManager.AppSettings["RestrictIpAddress"].ToString());
                if (restrictIpAddress && (requestIp != serializeModel.IpAddress))
                    isIpAddressValid = false;


                if (isIpAddressValid)
                {
                    curentUser.Id = serializeModel.Id;
                    curentUser.FullName = serializeModel.FullName;
                    curentUser.Department = serializeModel.Department;
                    curentUser.Email = serializeModel.Email;
                    curentUser.UserRole = serializeModel.UserRole;
                    curentUser.UserRoleDesc = serializeModel.UserRoleDesc;
                    curentUser.IpAddress = serializeModel.IpAddress;
                    HttpContext.Current.User = curentUser;
                }
            }
        }
    }
}