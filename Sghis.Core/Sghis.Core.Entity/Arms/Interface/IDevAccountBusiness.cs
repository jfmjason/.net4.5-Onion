using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Arms
{
    public interface IDevAccountBusiness
    {
        DevAccount GetDevAccountById(int id);
        DevRole GetAccountRoleById(int id);
    }
}
