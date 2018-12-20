using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace Sghis.Core.Api.Controllers
{
    [RoutePrefix("api/index")]
    public class IndexController : ApiController
    {

        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult Index()
        {
            return Ok("Test API");
        }

        [HttpGet]
        [Route("private")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Private()
        {
            return Ok("Test Private API");
        }
    }
}
