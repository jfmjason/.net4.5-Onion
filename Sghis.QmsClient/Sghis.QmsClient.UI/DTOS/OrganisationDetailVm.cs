using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sghis.QmsClient.UI.DTOS
{
    public class OrganisationDetailVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string IssueAuthorityCode { get; set; }
    }
}