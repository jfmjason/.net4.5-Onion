using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{
    public static partial class QmsMapper
    {

        public static List<TokenVm> MapTokenToVm(List<Token> objs)
        {
            var vms = new List<TokenVm>();

            foreach (var obj in objs)
            {
                vms.Add(MapTokenToVm(obj));
            }
            return vms;
        }
        public static TokenVm MapTokenToVm(Token obj)
        {
            var vm = new TokenVm()
            {
                Id = obj.Id,
                NewZoneId = obj.ZoneId.ToString(),
                TokenNumber = obj.TokenNumber,
                StatusId = ((int)obj.TokenStatus).ToString(),
                Status = obj.TokenStatus.DescriptionAttr(),
                Client = obj.ClientId.HasValue ? obj.Client.DeviceId.ToString() : "",
                Mrn = obj.Mrn
            };
            if (obj.Logs != null && obj.Logs.Count > 0)
            {
                if (obj.Logs.Count > 1)
                {
                    vm.OldZoneId = obj.Logs.Count > 0 ?
                        obj.Logs.OrderByDescending(c => c.Id).Skip(1).Take(1).Single()
                        .ZoneId.ToString() : "";
                }
                else
                {
                    vm.OldZoneId = obj.Logs.Count > 0 ? obj.Logs.Last().ZoneId.ToString() : "";
                }
            }
            return vm;
        }

        public static List<ZoneVm> MapZoneToVm(List<Zone> objs)
        {
            var vms = new List<ZoneVm>();

            foreach (var obj in objs)
            {
                vms.Add(MapZoneToVm(obj));
            }
            return vms;
        }
        public static ZoneVm MapZoneToVm(Zone obj)
        {
            var vm = new ZoneVm()
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name
            };
            return vm;
        }

        public static TokenVmWithLogs MapTokenToVmWithLogs(Token obj)
        {
            var vm = new TokenVmWithLogs()
            {
                Id = obj.Id,
                NewZoneId = obj.ZoneId.ToString(),
                TokenNumber = obj.TokenNumber,
                StatusId = ((int)obj.TokenStatus).ToString(),
                Status = obj.TokenStatus.DescriptionAttr(),
                Client = obj.ClientId.HasValue ? obj.Client.DeviceId.ToString() : "",
                OldZoneId = obj.Logs.Count > 0 ? obj.Logs.Last().ZoneId.ToString() : "",
            };
            vm.Logs = MapTokenLogToVm(obj.Logs);
            return vm;
        }

        public static List<TokenLogVm> MapTokenLogToVm(List<TokenLog> objs)
        {
            var vms = new List<TokenLogVm>();

            foreach (var obj in objs.OrderByDescending(c => c.Id))
            {
                vms.Add(MapTokenLogToVm(obj));
            }
            return vms;
        }
        public static TokenLogVm MapTokenLogToVm(TokenLog obj)
        {
            var vm = new TokenLogVm()
            {
                TokenStatus = obj.TokenStatus.DescriptionAttr(),
                Client = obj.ClientId.HasValue ? obj.Client.DeviceId.ToString() : "",
                Zone = obj.ZoneId.HasValue ? obj.Zone.Name.ToString() : "",
                ActionBy = obj.ActionBy,
                ActionAt = obj.ActionAt.ToString("MMM-dd-yyyy hh:mm tt")
            };
            return vm;
        }

        public static List<DtTokenResponse> MapTokenToDtContent(List<Token> objs)
        {
            var objsOut = new List<DtTokenResponse>();
            foreach (var obj in objs)
            {
                objsOut.Add(MapTokenToDtContent(obj));
            }
            return objsOut;
        }
        public static DtTokenResponse MapTokenToDtContent(Token obj)
        {
            var dto = new DtTokenResponse
            {
                DT_RowId = obj.Id,
                Id = obj.Id,
                NewZoneId = obj.ZoneId.ToString(),
                TokenNumber = obj.TokenNumber,
                StatusId = ((int)obj.TokenStatus).ToString(),
                Status = obj.TokenStatus.DescriptionAttr(),
                Client = obj.ClientId.HasValue ? obj.Client.DeviceId.ToString() : "",
                Mrn = obj.Mrn
            };
            return dto;
        }


    }
}
