using Sghis.Core.Entity.Qms;
using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Qms
{
    public interface IQmsDataManager
    {
        IRepository<TokenSeed> TokenSeed { get; }
        IRepository<Token> Token { get; }
        IRepository<TokenLog> TokenLog { get; }
        IRepository<Client> Client { get; }
        IRepository<ClientConnection> ClientConnection { get; }
        IRepository<Zone> Zone { get; }
    }
}
