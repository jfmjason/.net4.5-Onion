using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.Core.Entity.QmsClient.Model;
using System;
using System.Text;

namespace Sghis.QmsClient.Infra.Business
{
    public class QmsClientMasterFileBusiness : IQmsClientMasterFileBusiness
    {
        private IQmsClientDataManager _dataManager;

        public QmsClientMasterFileBusiness(IQmsClientDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public QmsClientOrganisationDetail GetOrganisationDetailById(int id)
        {
            var query = "SELECT Id, Name, City, IssueAuthorityCode FROM OrganisationDetails WHERE Id  = " + id;

            var detail = _dataManager.OrganizationDetail.Get(query, null);

            return detail;
        }

        public QmsClientPatient GetPatientByRegistrationNo(int registrationNo)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine(" SELECT  a.RegistrationNo, a.FirstName + ' ' +  a.MiddleName + ' ' +  a.LastName as Name, a.IssueAuthorityCode , CONVERT (INT, a.Age) Age, CONVERT(INT, a.AgeType) as AgetTypeId, b.name SexDescription, c.Name AgetTypeDescription, CONVERT(INT, b.id) AS SexId");
            query.AppendLine(" FROM Patient a");
            query.AppendLine(" JOIN sex b ON b.id = a.sex");
            query.AppendLine(" JOIN agetype c ON c.id = a.agetype");
            query.AppendLine(" WHERE RegistrationNo =" + registrationNo);

            var patient = _dataManager.Patient.Get(query.ToString(), null);

            return patient;
        }
    }
}
