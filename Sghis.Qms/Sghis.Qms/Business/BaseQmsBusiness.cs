using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Qms.Data;
using Sghis.Core.Entity.Base;

namespace Sghis.Qms.Business
{
    public abstract class BaseQmsBusiness
    {
        public IQmsDataManager Data;

        public BaseQmsBusiness(IQmsDataManager mgr)
        {
            Data = mgr;
        }
    }

}
