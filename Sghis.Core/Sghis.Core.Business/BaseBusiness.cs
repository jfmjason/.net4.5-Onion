using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Core.Entity.Base;

namespace Sghis.Core.Business
{
    public abstract class BaseBusiness
    {
        public IDataManager Data;

        public BaseBusiness(IDataManager mgr)
        {
            Data = mgr;
        }
    }

}
