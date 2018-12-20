using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.QmsClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.QmsClient.Interface
{
    public interface IQmsClientDataManager
    {
        IDbHelper<QmsClientPatient> Patient { get;  }
        IDbHelper<QmsClientOrganisationDetail> OrganizationDetail { get; }
        IDbHelper<QmsClientMenuAccess> MenuAccess { get; }
    }
}
