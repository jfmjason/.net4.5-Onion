using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{
    public interface IQueuingBusiness
    {
        Token GenerateToken(string mrn, int serviceTypeId, string ipAddress);
        Token GenerateToken(string mrn, int serviceTypeId, string ipAddress, string actionBy);
        Token GetToken(int id);
        Token GetTokenCurrrentlyServing(string ipAddress);

        List<Token> GetTokens(string ipAddress, string statusIds = null);
        List<Token> GetTokensByIpAddress(out int totalRecords, int startPage, int pageSize, string ipAddress = null, string statusIds = null);

        Token UpdateToken(int id, TokenStatus status, string actionBy, int? zoneId = null, int? clientId = null);
        Token UpdateTokenByIp(int id, TokenStatus status, string actionBy, int? zoneId = null, string ipAddress = null);

        int ClientConnected(string connectionId, Client client);
        Client GetClientByIpAddress(string ipAddress);
        bool DeleteConnection(string conId);

        List<ClientConnection> GetClientWithinZoneById(int zoneId);
        List<ClientConnection> GetClientWithinZoneByClientId(int clientId);
    }
}
