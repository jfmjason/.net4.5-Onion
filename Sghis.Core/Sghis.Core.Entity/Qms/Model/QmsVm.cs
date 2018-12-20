using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{

    public class TokenVm
    {
        public int Id { set; get; }
        public string TokenNumber { set; get; }
        public string Client { set; get; }
        public string NewZoneId { set; get; }
        public string OldZoneId { set; get; }
        public string StatusId { set; get; }
        public string Status { set; get; }
        public string Mrn { set; get; }
    }

    public class TokenVmWithLogs : TokenVm
    {
        public TokenVmWithLogs()
        {
            Logs = new List<TokenLogVm>();
        }
        public List<TokenLogVm> Logs { set; get; }
    }

    public class TokenLogVm
    {
        public string Zone { set; get; }
        public string Client { set; get; }
        public string TokenStatus { set; get; }
        public string ActionBy { set; get; }
        public string ActionAt { set; get; }
    }

    public class ClientVm
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string IpAddress { set; get; }
        public string ZoneId { set; get; }
        public string ZoneName { set; get; }
        public string ApiVersion { set; get; }
    }

    public class ZoneVm
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
    }

    public class DtTokenRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Status { set; get; }
    }

    public class DtTokenResponse
    {
        public int DT_RowId { get; set; } //https://datatables.net/examples/server_side/ids.html
        public int Id { set; get; }
        public string TokenNumber { set; get; }
        public string Client { set; get; }
        public string NewZoneId { set; get; }
        public string OldZoneId { set; get; }
        public string StatusId { set; get; }
        public string Status { set; get; }
        public string Mrn { set; get; }
    }
}
