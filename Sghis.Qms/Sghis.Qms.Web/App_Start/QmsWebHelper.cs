using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sghis.Qms.Web
{
    public static class QmsWebHelper
    {
        public static IHtmlString BaseUrl(this HtmlHelper helper)
        {
            string baseUrl = "";
            baseUrl = VirtualPathUtility.ToAbsolute("~/");
            return new HtmlString(baseUrl);
        }
    }
}