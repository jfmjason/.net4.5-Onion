using AutoMapper;
using Sghis.Core.Entity.QmsClient.Interface;
using Sghis.QmsClient.UI.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Sghis.QmsClient.UI.Controllers.api
{
   //[RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        IQmsClientMasterFileBusiness _masterfileBusiness;
        IMapper _mapper;
        public PatientsController(IQmsClientMasterFileBusiness business, IMapper mapper)
        {
            _masterfileBusiness = business;
            _mapper = mapper;
        }
        [HttpGet]
        public HttpResponseMessage Get(int registrationno)
        {
            var patient = _mapper.Map<PatientVm>(_masterfileBusiness.GetPatientByRegistrationNo(registrationno));

            var response = Request.CreateResponse(HttpStatusCode.OK);

            if (patient.RegistrationNo > 0)
                response.Content = new StringContent(new JavaScriptSerializer().Serialize(patient), Encoding.UTF8, "application/json");

            else
                response.Content = new StringContent("{}");


            return response;

        }
    }
}