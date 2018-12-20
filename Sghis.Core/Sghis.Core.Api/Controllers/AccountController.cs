using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Sghis.Core.Api.Controllers
{
    public class AccountController : Controller
    {

        public AccountController()
        {
        }

        // GET: home/index
        public ActionResult Index(string callbackUrl)
        {
            ViewBag.CallBackUrl = callbackUrl;
            return View();
        }

        // GET: home/index
        public async Task<HttpResponseMessage> Login(string callBackUrl)
        {
            // Invoke the "token" OWIN service to perform the login: /api/token
            // Ugly hack: I use a server-side HTTP POST because I cannot directly invoke the service (it is deeply hidden in the OAuthAuthorizationServerHandler class)
            var request = Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "token";
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", "beewan"),
                        new KeyValuePair<string, string>("password", "beewan"),
                        new KeyValuePair<string, string>("client_id", "1")
                    };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                if (responseCode == System.Net.HttpStatusCode.OK)
                {
                    Response.Redirect(callBackUrl + "?token=" + HttpUtility.UrlEncode(responseString));
                }

                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };
                return responseMsg;

            }
        }

    }
}
