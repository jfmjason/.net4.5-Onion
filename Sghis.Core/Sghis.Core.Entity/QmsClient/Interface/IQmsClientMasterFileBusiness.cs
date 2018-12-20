

using Sghis.Core.Entity.QmsClient.Model;

namespace Sghis.Core.Entity.QmsClient.Interface
{
    public interface IQmsClientMasterFileBusiness
    {
        QmsClientPatient GetPatientByRegistrationNo(int registrationNo);

        QmsClientOrganisationDetail GetOrganisationDetailById(int id);
    }
}
