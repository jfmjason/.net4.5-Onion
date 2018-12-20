using Sghis.Core.Entity.Qms;
using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Qms.Data {

    public class QmsDataManager : IQmsDataManager
    {
        private QmsDbContext context;

        public IRepository<Token> Token { get; private set; }
        public IRepository<TokenSeed> TokenSeed { get; private set; }
        public IRepository<TokenLog> TokenLog { get; private set; }
        public IRepository<Client> Client { get; private set; }
        public IRepository<ClientConnection> ClientConnection { get; private set; }
        public IRepository<Zone> Zone { get; private set; }

        //DbContext is not a thread safe, 
        //So whenever we need datamanager, we will create a new instance of DbContext
        public QmsDataManager() {
            context = new QmsDbContext();
            Token = new QmsRepository<Token>(context);
            TokenSeed = new QmsRepository<TokenSeed>(context);
            TokenLog = new QmsRepository<TokenLog>(context);
            Client = new QmsRepository<Client>(context);
            ClientConnection = new QmsRepository<ClientConnection>(context);
            Zone = new QmsRepository<Zone>(context);
        }
    }
}
