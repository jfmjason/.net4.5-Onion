using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sghis.QmsClient.UI.DTOS
{
    public class MenuVm
    {
        public int Id { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuId { get; set; }

        public string ParentMenuName { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
    }
}