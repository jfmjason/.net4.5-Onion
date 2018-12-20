using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Common
{

    public class User : BaseModelGrouped
    {
        [StringLength(99)]
        public string UserName { set; get; }
        [StringLength(256)]
        public string Password { set; get; }
        [StringLength(99)]
        public string Email { set; get; }
        [StringLength(99)]
        public string Department { set; get; }
        [StringLength(99)]
        public string FullName { set; get; }
    }

}
