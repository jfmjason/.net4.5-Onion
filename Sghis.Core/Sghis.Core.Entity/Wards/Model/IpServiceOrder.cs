using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Wards
{
    public class IpServiceOrder : BaseWardsGrouped
    {
        public IpServiceOrder()
        {
            IpServiceOrderDetail = new List<IpServiceOrderDetail>();
        }
        public int IpId { set; get; }
        public int? IpbServiceId { set; get; }
        public int? CompanyId { set; get; }
        public int? CategoryId { set; get; }
        public int? GradeId { set; get; }
        public int? TariffId { set; get; }
        public int? BedId { set; get; }

        [StringLength(512)]
        public string PatientName { set; get; }
        public int? PatientNationalityId { set; get; }
        public DateTime? PatientBirthDate { set; get; }
        public double? PatientAge { set; get; }
        public Religion? PatientReligionId { set; get; }
        public Gender? PatientGenderId { set; get; }
        public MaritalStatus? PatientMaritalStatusId { set; get; }

        public int? PrimaryDoctorId { set; get; }
        public int? PrimaryDoctorNationalityId { set; get; }
        public DateTime? PrimaryDoctorBirthDate { set; get; }
        public Religion? PrimaryDoctorReligionId { set; get; }
        public string PrimaryDoctorReligionDesc { get { return PrimaryDoctorReligionId == null ? "" : PrimaryDoctorReligionId.DescriptionAttr(); } }
        public Gender? PrimaryDoctorGenderId { set; get; }
        public MaritalStatus? PrimaryDoctorMaritalStatusId { set; get; }
        public int? PrimaryDoctorDepartmentId { set; get; }

        public int? OrderingDoctorId { set; get; }
        public DateTime? OrderingDoctorBirthDate { set; get; }
        public Religion? OrderingDoctorReligionId { set; get; }
        public string OrderingDoctorReligionDesc { get { return OrderingDoctorReligionId == null ? "" : OrderingDoctorReligionId.DescriptionAttr(); } }
        public Gender? OrderingDoctorGenderId { set; get; }
        public MaritalStatus? OrderingDoctorMaritalStatusId { set; get; }
        public int? OrderingDoctorDepartmentId { set; get; }
        public int? StationId { set; get; }
        

        public virtual List<IpServiceOrderDetail> IpServiceOrderDetail { set; get; }
    }

    public class IpServiceOrderDetail : BaseWards
    {
        public virtual IpServiceOrder IpServiceOrder { set; get; }
        public int? IpServiceOrderId { set; get; }

        public int IpId { set; get; }
        public int? SerialNo { set; get; }
        public int? IpbServiceId { set; get; }
        public int? CompanyId { set; get; }
        public int? CategoryId { set; get; }
        public int? GradeId { set; get; }
        public int? TariffId { set; get; }

        public int? OrderingDoctorId { set; get; }
        public int? OrderId { set; get; }
        public int? ItemDepartmentId { set; get; }
        public int? ItemId { set; get; }
        [StringLength(512)]
        public string ItemCode { set; get; }
        [StringLength(512)]
        public string ItemName { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public int? TaxId { set; get; }
        public decimal TaxPercentage { get; set; }

        public int? BedId { set; get; }
        public int? BedTypeId { set; get; }
        public int? PackageId { set; get; }
        public int? SurgeryTypeId { set; get; }

        public DateTime? OrderDateTime { set; get; }
        public DateTime? CancelDateTime { set; get; }
        public DateTime? EntryDateTime { set; get; }
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
        public decimal? ExemptionPatientTaxAmount { get; set; }

    }

    public class IpServiceOrderDetailLog : BaseWards
    {
        public int IpId { set; get; }
        public int OrderId { set; get; }
        public int ServiceId { set; get; }
        [StringLength(512)]
        public string Remarks { set; get; }
    }
}
