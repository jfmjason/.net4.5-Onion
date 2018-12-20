using AutoMapper;
using Sghis.Core.Entity.QmsClient.Model;
using Sghis.QmsClient.UI.DTOS;

namespace Sghis.QmsClient.UI.Common
{
    public class MasterFileMappingProfile : Profile
    {
        public MasterFileMappingProfile() { 

        CreateMap<QmsClientOrganisationDetail, OrganisationDetailVm>();
        CreateMap<QmsClientPatient, PatientVm>();

        }
    }


    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<QmsClientMenuAccess, MenuVm>();
        }
    }
}