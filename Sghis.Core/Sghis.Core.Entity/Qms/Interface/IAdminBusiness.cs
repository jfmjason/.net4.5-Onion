using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{
    public interface IAdminBusiness
    {
        List<Zone> GetZones(out int totalRecords, int startPage, int pageSize);
        List<Zone> GetZones();
        Zone GetZoneById(int id);
        Zone GetZoneByIpAddress(string ipAddress);
        List<Zone> GetZonesByIpAddress(string ipAddress);
        Zone GetZoneByClientId(int clientId);
        bool AddZone(Zone zone);
        bool AddZone(string name, string code, string actionBy);
        bool UpdateZone(Zone zone, string actionBy);
        bool DeleteZone(int id, string actionBy);

        List<Client> GetClients(out int totalRecords, int startPage, int pageSize);
        Client GetClientById(int id);
        Client GetClientByIpAddress(string ipAddress);
        bool AddClient(Client client);
        bool AddClient(ClientType type, string name, string actionBy);
    }
}
