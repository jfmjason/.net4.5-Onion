namespace Sghis.QmsClient.UI.DTOS
{
    public class PatientVm
    {
        public int RegistrationNo { get; set; }
        public int Age { get; set; }
        public int AgetTypeId { get; set; }
        public int SexId { get; set; }

        public string AgetTypeDescription { get; set; }
        public string Name { get; set; }
        public string SexDescription { get; set; }
        public string IssueAuthorityCode { get; set; }

        public string PIN { get { return IssueAuthorityCode + "." + RegistrationNo.ToString("000000000"); } }
    }
}