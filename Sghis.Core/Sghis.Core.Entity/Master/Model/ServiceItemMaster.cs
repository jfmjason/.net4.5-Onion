using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Master
{
    public class ServiceItemMaster : BaseMasterGrouped
    {
        //0 - Inpatient; 1 - Outpatient
        public int PatientType { set; get; }
        public int ServiceId { set; get; }
        public int DepartmentId { set; get; }
        public int ItemId { set; get; }
        [StringLength(512)]
        public string ItemCode { set; get; }
        [StringLength(512)]
        public string ItemName { set; get; }
        [StringLength(512)]
        public string ItemDescription { set; get; }
        [StringLength(512)]
        public string ItemCodeArabic { set; get; }
        [StringLength(512)]
        public string ItemNameArabic { set; get; }
        [StringLength(512)]
        public string ItemDescriptionArabic { set; get; }
        public decimal CostPrice { set; get; }
        [StringLength(512)]
        public string OraCode { set; get; }
        public int? TaxId { set; get; }
    }
}
