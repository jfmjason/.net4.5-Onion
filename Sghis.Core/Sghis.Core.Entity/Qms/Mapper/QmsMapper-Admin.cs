using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{
    public static partial class QmsMapper
    {

        public static List<ClientVm> MapClientToVm(List<Client> objs)
        {
            var vms = new List<ClientVm>();

            foreach (var obj in objs)
            {
                vms.Add(MapClientToVm(obj));
            }
            return vms;
        }

        public static ClientVm MapClientToVm(Client obj)
        {
            var vm = new ClientVm()
            {
                Id = obj.Id,
                IpAddress = obj.IpAddress,
                Name = obj.DeviceId,
                ZoneName = obj.Zone.Name,
                ZoneId = obj.ZoneId.ToString()
            };
            return vm;
        }



    }
}
