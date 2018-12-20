
using AutoMapper;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.QmsClient.UI.DTOS;
using System.Web.Configuration;
using LightInject;

namespace Sghis.QmsClient.UI.Common
{
    public class Global
    {
        static IQmsClientMasterFileBusiness _masterfileBusiness = LightningMcQueen.Container.GetInstance<IQmsClientMasterFileBusiness>();
        static IMapper _mapper = LightningMcQueen.Container.GetInstance<IMapper>();

        private static OrganisationDetailVm _organisationDetails;
        private static string qmshubAddress;
        private static string qmsapiAddress;
        private static int? moduleId;

        public static OrganisationDetailVm OrganisationDetails
        {
          get
            {
                if (_organisationDetails == null)
                {
                    var orgdetail = _masterfileBusiness.GetOrganisationDetailById(1);

                    _organisationDetails = _mapper.Map<OrganisationDetailVm>(orgdetail);

                }
                return _organisationDetails;
            }
        }

        public static string QmsHubAddress
        {
            get
            {
                if (qmshubAddress == null)
                {
                    qmshubAddress = WebConfigurationManager.AppSettings["qms:hub"].ToString();
                }

                return qmshubAddress;
            }

        }

        public static string QmsApiAddress
        {
            get
            {
                if (qmsapiAddress == null)
                {
                    qmsapiAddress = WebConfigurationManager.AppSettings["qms:api"].ToString();
                }

                return qmsapiAddress;
            }

        }

        public static int ModuleId {
            get
            {
                if (moduleId == null)
                {
                    moduleId = int.Parse( WebConfigurationManager.AppSettings["module:id"].ToString());
                }

                return moduleId.Value;
            }
        }
    }
}