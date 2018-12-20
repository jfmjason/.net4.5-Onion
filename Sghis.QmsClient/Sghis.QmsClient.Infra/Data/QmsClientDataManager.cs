using Sghis.Core.Entity.QmsClient;
using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.Core.Entity.QmsClient.Model;

namespace Sghis.QmsClient.Infra.Data
{

    public class QmsClientDataManager : IQmsClientDataManager
    {
        private readonly QmsClientDbContext context;

        public IDbHelper<QmsClientPatient> Patient => new QmsClientDbHelper<QmsClientPatient>(context);

        public IDbHelper<QmsClientOrganisationDetail> OrganizationDetail => new QmsClientDbHelper<QmsClientOrganisationDetail>(context);

        public IDbHelper<QmsClientMenuAccess> MenuAccess => new QmsClientDbHelper<QmsClientMenuAccess>(context);

        //DbContext is not a thread safe, 
        //So whenever we need datamanager, we will create a new instance of DbContext
        public QmsClientDataManager()
        {
            context = new QmsClientDbContext();
        }
    }
}
