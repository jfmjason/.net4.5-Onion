using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Master;
using Sghis.Core.Entity.Wards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Base {

    public interface IDataManager {
        IRepository<User> User { get; }
        IRepository<Client> Client { get; }

        IRepository<IpServiceOrder> IpServiceOrder { get; }
        IRepository<IpPrice> IpPrice { get; }

        IDbHelper<UserSp> UserSp { get; }
    }
}
