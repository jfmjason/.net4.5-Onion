using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace Sghis.Core.Api.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        // GET: home/index
        public ActionResult Index()
        {
            return View();
        }

    }
}
