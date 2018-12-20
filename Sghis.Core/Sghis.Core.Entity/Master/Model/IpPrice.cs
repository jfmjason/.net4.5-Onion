using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Master
{
    public class IpPrice : BaseMasterGrouped
    {
        public int TariffId { set; get; }
        public int BedTypeId { set; get; }
        public int IpbServiceId { set; get; }
        public int ItemId { set; get; }
        public decimal? Price { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { set; get; }
    }
}
