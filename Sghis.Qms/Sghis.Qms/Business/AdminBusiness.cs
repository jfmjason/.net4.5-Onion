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


    public class AdminBusiness : BaseQmsBusiness, IAdminBusiness
    {

        public AdminBusiness(IQmsDataManager mgr) : base(mgr) { }

        public List<Zone> GetZones(out int totalRecords, int startPage, int pageSize)
        {
            var query = Data.Zone.GetAllNoTrack();
            totalRecords = query.Count();
            return query.OrderByDescending(c => c.Code).Skip(startPage).Take(pageSize).ToList();
        }


        public List<Zone> GetZones()
        {
            var query = Data.Zone.GetAllNoTrack();
  
            return query.Where(c => c.Deleted == false).ToList() ;
        }
        public Zone GetZoneById(int id)
        {
            return Data.Zone.GetById(id);
        }
        public Zone GetZoneByClientId(int clientId)
        {
            return Data.Client.GetById(clientId).Zone;
        }

        public Zone GetZoneByIpAddress(string ipddress)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipddress);
            return GetZoneByClientId(client.Id);
        }

        public List<Zone> GetZonesByIpAddress(string ipddress)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipddress);
            var query = Data.Zone.GetAllNoTrack();
            return query.ToList();
        }

        public bool AddZone(Zone zone)
        {
            throw new NotImplementedException();
        }

        public bool AddZone(string name, string code, string actionBy)
        {
            throw new NotImplementedException();
        }

        public bool UpdateZone(Zone zone, string actionBy)
        {
            throw new NotImplementedException();
        }

        public bool DeleteZone(int id, string actionBy)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetClients(out int totalRecords, int startPage, int pageSize)
        {
            var query = Data.Client.GetAllNoTrack();
            totalRecords = query.Count();
            return query.OrderByDescending(c => c.DeviceId).Skip(startPage).Take(pageSize).ToList();
        }

        public Client GetClientById(int clientId)
        {
            return Data.Client.GetById(clientId);
        }

        public Client GetClientByIpAddress(string ipddress)
        {
            var client = Data.Client.GetByCriteria(c => c.IpAddress == ipddress);
            return GetClientById(client.Id);
        }

        public bool AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public bool AddClient(ClientType type, string name, string actionBy)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClient(Zone zone, string actionBy)
        {
            throw new NotImplementedException();
        }

       
    }
}
