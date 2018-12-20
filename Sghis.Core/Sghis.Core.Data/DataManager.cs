using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Wards;
using Sghis.Core.Entity.Master;

namespace Sghis.Core.Data {
    public class DataManager : IDataManager {
        private SghisCoreDbContext context;

        public IRepository<User> User { get; private set; }
        public IRepository<Client> Client { get; private set; }

        public IRepository<IpServiceOrder> IpServiceOrder { get; private set; }
        public IRepository<IpPrice> IpPrice { get; private set; }

        //Storedprocedures objects
        public IDbHelper<UserSp> UserSp { get; private set; }
        //DbContext is not a thread safe, 
        //So whenever we need datamanager, we will create a new instance of DbContext
        public DataManager() {
            context = new SghisCoreDbContext();
            User = new SghisRepository<User>(context);
            Client = new SghisRepository<Client>(context);

            IpServiceOrder = new SghisRepository<IpServiceOrder>(context);
            IpPrice = new SghisRepository<IpPrice>(context);
            UserSp = new DbHelper<UserSp>(context);
        }
    }
}
