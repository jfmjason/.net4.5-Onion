using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Wards.Model
{
    public class IPServiceOrderAdjustment : BaseWards
    {
        public IPServiceOrderAdjustment()
        {
            IpServiceOrderDetail = new IpServiceOrderDetail();
        }
        public int? IpId { get; set; }
        public int? SerialNo { get; set; }
        public int IpbServiceId { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public int GradeId { get; set; }
        public int TrariffId { get; set; }
        public int OrderingDoctorId { get; set; }
        public int OrderId { get; set; }
        public int ItemDepartmentId { get; set; }
        public int ItemId { get; set; }
        [StringLength(512)]
        public string ItemCode { set; get; }
        [StringLength(512)]
        public string ItemName { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public decimal PriceDifference { set; get; }  //adjusted amount either + or -
        public int? TaxId { set; get; }
        public decimal? TaxPercentage { get; set; }
        public int? BedId { get; set; }
        public int? BedTypeId{ get; set; }
        public int? PackageId { get; set; }
        public int? SurgeryTypeId { get; set; }
        public DateTime? OrderDateTime{ get; set; }
        public DateTime? CancelDateTime { get; set; }
        public DateTime? EntryDateTime { get; set; }

        public decimal? ProfileMarkupPercentage { get; set; }
        public decimal? ProfileMarkupAmount { get; set; }
        public decimal? MarkupAmount { get; set; }

        public decimal? ProfileDiscountPercentage { get; set; }
        public decimal? ProfileDiscountAmount { get; set; }
        public int? ProfileDiscountType { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ProfileDeductiblePercentage { get; set; }
        public decimal? ProfileDeductibleAmount { get; set; }
        public int? ProfileDeductibleType { get; set; }
        public decimal? DeductibleAmount { get; set; }
        public decimal? MaxDeductibleAmount { get; set; }
        public decimal? CompanyTaxAmount { get; set; }
        public decimal? PatientDeductibleTaxAmount { get; set; }
        public decimal? PatientExclusionTaxAmount { get; set; }
        public decimal? PatientTaxAmount { get; set; }
        public decimal? ExemptionPatientDeductibleTaxAmount { get; set; }
        public decimal? ExemptionPatientExclusionTaxAmount { get; set; }
        public decimal ExemptionPatientTaxAmount { get; set; }

        public virtual IpServiceOrderDetail IpServiceOrderDetail { set; get; }
        
    }
}
