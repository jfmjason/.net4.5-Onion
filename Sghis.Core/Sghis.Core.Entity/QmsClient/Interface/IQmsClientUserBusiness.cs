using Sghis.Core.Entity.QmsClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.QmsClient.Interface
{
    public interface IQmsClientUserBusiness
    {
        IEnumerable<QmsClientMenuAccess> GetMenuAccess(int userId, int moduleId);
    }
}
