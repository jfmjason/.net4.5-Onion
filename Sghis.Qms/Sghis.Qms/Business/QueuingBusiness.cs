using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Qms.Data;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Qms;

namespace Sghis.Qms.Business
{


    public class QueuingBusiness : BaseQmsBusiness, IQueuingBusiness
    {

        public QueuingBusiness(IQmsDataManager mgr) : base(mgr) { }

        public int ClientConnected(string connectionId, Client client)
        {
            var clientDb = Data.Client.GetAll().Where(c => c.IpAddress == client.IpAddress && c.Deleted == false).First();
            if ((client.ClientType == ClientType.Display || client.ClientType == ClientType.Patient)
                && clientDb == null)
                throw new Exception("Cannot connect to QMS. Device is not registered.");
            var connection = new ClientConnection()
            {
                ConnectionId = connectionId,
                Active = true,
                ClientId = clientDb.Id,
                ConnectionDt = DateTime.Now
            };
            //foreach (var con in clientDb.Connections)
            //{
            //    con.Active = false;
            //    Data.ClientConnection.Update(con);
            //}

            clientDb.Connections.Add(connection);
            Data.Client.Update(client);
            Data.Client.Commit();
            return client.Id;
        }

        public bool DeleteConnection(string conId)
        {
            var connection = Data.ClientConnection.GetByCriteria(c => c.ConnectionId == conId);
            if (connection != null)//Delete
            {
                Data.ClientConnection.Delete(connection);
            }
            Data.ClientConnection.Commit();
            return true;
        }

        public Token GenerateToken(string mrn, int serviceTypeId, string ipAddress)
        {
            return GenerateToken(mrn, serviceTypeId, ipAddress, null);
        }

        public Token GenerateToken(string mrn, int serviceTypeId, string ipAddress, string actionBy)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress);
            if (client == null)
                throw new Exception("Ip address doesn't exist.");

            var seed = Data.TokenSeed.GetById(serviceTypeId);
            var tokenLog = new TokenLog()
            {
                ZoneId = client.ZoneId,
                ActionBy = actionBy,
                TokenStatus = TokenStatus.Pending
            };

            var token = new Token()
            {
                Mrn = mrn,
                Number = seed.Number + 1,
                ServiceTypeId = serviceTypeId,
                Prefix = seed.Zone.Code,
                ZoneId = client.ZoneId.Value,
                TokenStatus = TokenStatus.Pending
            };
            token.Logs.Add(tokenLog);

            seed.Number += 1;
            Data.TokenSeed.Update(seed);
            Data.Token.Add(token);
            Data.Token.Commit();
            return token;
        }

        public Token GetToken(int id)
        {
            return Data.Token.GetById(id);
        }

        public Token GetTokenCurrrentlyServing(string ipAddress)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress);
            if (client == null)
                throw new Exception("Invalid or unregistered ip address.");
            var count = Data.Token.GetAll().Where(c => c.ClientId == client.Id && c.TokenStatus == TokenStatus.Serving).OrderByDescending(c => c.Id).Count();
            //&& c.ActionAt < DateTime.Now.AddDays(1) && c.ActionAt > DateTime.Now.AddDays(-1)).Count();
            if (count == 0)
                return null;
            return Data.Token.GetAll().Where(c => c.ClientId == client.Id && c.TokenStatus == TokenStatus.Serving).OrderByDescending(c => c.Id).FirstOrDefault();
        }

        public List<Token> GetTokens(string ipAddress, string statusIds = null)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress);
            var query = Data.Token.GetAllNoTrack().Where(c => c.ZoneId == client.ZoneId);
            if (!string.IsNullOrEmpty(statusIds))
            {
                var ids = statusIds.Split(',');
                query = query.Where(c => ids.Contains(((int)c.TokenStatus).ToString()));
            }
            return query.ToList();
        }

        public List<Token> GetTokensByIpAddress(out int totalRecords, int startPage, int pageSize, string ipAddress = null, string statusIds = null)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress);
            var query = Data.Token.GetAllNoTrack().Where(c => c.ZoneId == client.ZoneId);

            if (!string.IsNullOrEmpty(statusIds))
            {
                var ids = statusIds.Split(',');
                query = query.Where(c => ids.Contains(((int)c.TokenStatus).ToString()));
            }

            totalRecords = query.Count();
            return query.OrderByDescending(c => c.ActionAt).OrderBy(c => c.TokenStatus).Skip(startPage).Take(pageSize).ToList();
        }

        public Token UpdateTokenByIp(int id, TokenStatus status, string actionBy, int? zoneId = null, string ipAddress = null)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress);
            return UpdateToken(id, status, actionBy, zoneId, client.Id);
        }

        public Token UpdateToken(int id, TokenStatus status, string actionBy, int? zoneId = null, int? stationId = null)
        {
            var token = Data.Token.GetById(id);
            if (token == null)
                throw new Exception("Token id doesn't exist.");

            if (status == TokenStatus.Transfered)
            {
                token.ZoneId = zoneId.Value;
                token.ClientId = null;
            }

            if (status == TokenStatus.Serving)
                token.ClientId = stationId.Value;

            var tokenLog = new TokenLog()
            {
                ZoneId = token.ZoneId,
                ClientId = token.ClientId,
                ActionBy = actionBy,
                TokenStatus = status,
                Token = token
            };


            token.TokenStatus = status;
            token.ActionBy = actionBy;
            token.ActionAt = DateTime.Now;

            if (status == TokenStatus.Delayed)
            {
                token.ActionAt = token.ActionAt.AddMinutes(5);
            }

            token.Logs.Add(tokenLog);
            Data.Token.Update(token);
            Data.Token.Commit();
            return token;
        }

        #region Admin
        public List<Zone> GetZones(out int totalRecords, int startPage, int pageSize)
        {
            var query = Data.Zone.GetAllNoTrack();
            totalRecords = query.Count();
            return query.OrderByDescending(c => c.Code).Skip(startPage).Take(pageSize).ToList();
        }

        public List<Client> GetClients(out int totalRecords, int startPage, int pageSize)
        {
            var query = Data.Client.GetAllNoTrack();
            totalRecords = query.Count();
            return query.OrderByDescending(c => c.DeviceId).Skip(startPage).Take(pageSize).ToList();
        }

        public Client GetClientByIpAddress(string ipAddress)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipAddress.Trim());
            if (client == null)
                throw new Exception("Cannot connect to QMS. Device is not registered.");

            return client;
        }

        public List<ClientConnection> GetClientWithinZoneById(int zoneId)
        {
            return Data.ClientConnection.GetAll().Where(c => c.Active && c.Client.ZoneId == zoneId).ToList();
        }

        public List<ClientConnection> GetClientWithinZoneByClientId(int clientId)
        {
            var client = Data.Client.GetById(clientId);
            return Data.ClientConnection.GetAll().Where(c => c.Active && c.Client.ZoneId == client.ZoneId).ToList();
        }

        #endregion

    }
}
