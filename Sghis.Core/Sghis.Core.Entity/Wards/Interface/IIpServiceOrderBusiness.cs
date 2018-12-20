using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Wards
{
    public interface IIpServiceOrderBusiness
    {
        IpServiceOrderDetail GetIpServiceOrderDetailById(int id);
    }


}
