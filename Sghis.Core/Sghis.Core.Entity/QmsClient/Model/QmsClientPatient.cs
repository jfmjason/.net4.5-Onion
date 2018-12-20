using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.QmsClient.Model
{
    public class QmsClientPatient
    {
        public int RegistrationNo { get; set; }
        public int Age { get; set; }
        public int AgetTypeId { get; set; }
        public int SexId { get; set; }

        public string AgetTypeDescription { get; set; }
        public string Name { get; set; }
        public string SexDescription { get; set; }
        public string IssueAuthorityCode { get; set; }
    }
}
