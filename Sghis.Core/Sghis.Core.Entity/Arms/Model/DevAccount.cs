using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Arms
{
    public class DevAccount : BaseArmsSimple
    {
        [StringLength(99)]
        public string UserName { set; get; }
        [StringLength(99)]
        public string Email { set; get; }
        [StringLength(99)]
        public string Department { set; get; }
        [StringLength(99)]
        public string FullName { set; get; }
    }

    public class DevRole : BaseArmsGrouped
    {
        [StringLength(99)]
        public string Name { set; get; }
    }

    public class AccountRoleMapping : BaseArmsSimple
    {
        public virtual DevAccount DevAccount { set; get; }
        public virtual int? DevAccountId { set; get; }
        public virtual DevRole DevRole { set; get; }
        public virtual int? DevRoleId { set; get; }

        public virtual List<AccountRoleMenuMapping> AccountRoleMenuMapping { set; get; }
    }

    public class DevMenu : BaseArmsSimple
    {
        [StringLength(99)]
        public string Name { set; get; }
    }

    public class AccountRoleMenuMapping : BaseArmsSimple
    {
        public virtual AccountRoleMapping AccountRoleMapping { set; get; }
        public virtual int? AccountRoleMappingId { set; get; }

        public virtual DevMenu DevMenu { set; get; }
        public virtual int? DevMenuId { set; get; }
    }




}
