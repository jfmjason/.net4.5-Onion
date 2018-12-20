using AutoMapper;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.QmsClient.UI.Common;
using Sghis.QmsClient.UI.DTOS;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sghis.QmsClient.UI.Controllers
{
    public class HomeController : Controller
    {
        IQmsClientUserBusiness _userBusiness;
        IMapper _imapper;

        public HomeController(IQmsClientUserBusiness userBusiness, IMapper mapper)
        {
            _userBusiness = userBusiness;
            _imapper = mapper;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Menus() {

            var claims = (CustomPrincipal)User;
            var menus = _userBusiness.GetMenuAccess(claims.Id, Global.ModuleId);

            return PartialView("_Menus", _imapper.Map<List<MenuVm>>(menus));
        }

    }
}