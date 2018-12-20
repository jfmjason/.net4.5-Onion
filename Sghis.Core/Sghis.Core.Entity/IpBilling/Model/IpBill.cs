using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.IpBilling
{
    public class IpBill : BaseIpBillGrouped
    {
        public IpBill()
        {
            IpBillDetail = new List<IpBillDetail>();
        }
        public int IpId { set; get; }
        public virtual List<IpBillDetail> IpBillDetail { set; get; }
    }

    public class IpBillDetail : BaseIpBill
    {
        public int IpId { set; get; }
        public int? SerialNo { set; get; }
    }
}
