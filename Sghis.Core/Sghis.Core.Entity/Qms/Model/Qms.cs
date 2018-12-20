using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{
    public enum TokenStatus
    {
        Pending = 1,
        Serving = 2,
        Transfered = 3,
        Delayed = 4,
        TransferedByPatient = 5,
        Deleted = 9,
        Done = 10,
    };

    public enum SortDirection
    {
        Ascending = 1,
        Descending = 2
    };

    public enum ClientType
    {
        His = 1,
        Display = 2,
        Patient = 3
    };

    public class TokenSeed : BaseQmsSimple
    {
        public int ZoneId { set; get; }
        public virtual Zone Zone { set; get; }
        public int Number { set; get; }
    }

    public class Token : BaseQmsSimple
    {
        public Token()
        {
            Logs = new List<TokenLog>();
        }
        [StringLength(99)]
        public string Mrn { set; get; }
        [StringLength(99)]
        public string Prefix { set; get; }
        public int Number { set; get; }
        public string TokenNumber { get { return Prefix + Number; } }
        public virtual TokenStatus TokenStatus { set; get; }
        public virtual string TokenStatusDesc { get { return TokenStatus.DescriptionAttr(); } }

        public int ServiceTypeId { get; set; }
        public int ZoneId { set; get; }
        public virtual Zone Zone { set; get; }
        public int? ClientId { set; get; }
        public virtual Client Client { set; get; }
        public virtual List<TokenLog> Logs { set; get; }
    }

    public class TokenLog : BaseQmsSimple
    {
        public virtual Token Token { set; get; }
        public virtual int? TokenId { set; get; }
        public virtual int? ZoneId { set; get; }
        public virtual Zone Zone { set; get; }
        public virtual int? ClientId { set; get; }
        public virtual Client Client { set; get; }
        public virtual TokenStatus TokenStatus { set; get; }
        public virtual string TokenStatusDesc { get { return TokenStatus.DescriptionAttr(); } }
    }

    public class Client : BaseQmsSimple
    {
        public Client()
        {
            Connections = new List<ClientConnection>();
        }
        public ClientType ClientType { set; get; }
        [StringLength(99)]
        public string DeviceId { set; get; }
        [StringLength(99)]
        public string IpAddress { set; get; }
        public virtual List<ClientConnection> Connections { set; get; }
        public virtual Zone Zone { set; get; }
        public virtual int? ZoneId { set; get; }
    }

    public class ClientConnection
    {
        public virtual int Id { set; get; }
        public virtual string ConnectionId { set; get; }
        public virtual int? ClientId { set; get; }
        public virtual Client Client { set; get; }
        public virtual DateTime ConnectionDt { set; get; }
        public virtual bool Active { set; get; }
    }

    public class Zone : BaseQmsGrouped
    {
        [StringLength(99)]
        public virtual string Name { set; get; }
        [StringLength(99)]
        public virtual string Code { set; get; }
        public virtual List<Client> Clients { set; get; }
    }

    public class ClientService
    {
        public virtual int Id { set; get; }
        public virtual int ClientId { set; get; }
        public virtual Client Client { set; get; }
        public virtual int ServiceId { set; get; }
    }

    public class ZoneService
    {
        public virtual int Id { set; get; }
        public virtual int ZoneId { set; get; }
        public virtual Zone Zone { set; get; }
        public virtual int ServiceId { set; get; }
    }
}
