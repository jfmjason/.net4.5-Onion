﻿using System.Text;
using System.Web.Http;

namespace Sghis.Core.Api.Controllers
{
    [RoutePrefix("api/Authentication")]
    public class AuthenticationController : ApiController
    {

        [HttpGet]
        [Route("NoAuth")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult NoAuth()
        {
            //Accounts userRoles = new Accounts();
            //return Ok(userRoles.GetUserRoles());
            return Ok();
        }

        [HttpGet]
        [Route("AuthorizedUser")]
        [Authorize(Roles ="Admin")]
        public IHttpActionResult AuthorizedUser()
        {
            //Accounts userRoles = new Accounts();
            //return Ok(userRoles.GetUserRoles());3
            return Ok();
        }

        [HttpGet]
        [Route("AuthenticatedUser")]
        [Authorize]
        public IHttpActionResult AuthenticatedUser()
        {
            //Employees emp = new Employees();
            //return Ok(emp.GetEmployees());
            return Ok();
        }

        [HttpPost]
        [Route("PostUserData")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostData()
        {
            //Accounts regObj = new Accounts();
            //ApplicationUser regData = new ApplicationUser();
            //regData.EmailID = userData.EmailID;
            //regData.Password = Encoding.UTF8.GetBytes(userData.Password);
            //regData.UserName = userData.UserName;
            //regData.RoleId = userData.RoleId;

            //return Ok(regObj.Register(regData));
            return Ok();
        }

    }
}