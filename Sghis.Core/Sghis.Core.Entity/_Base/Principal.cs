using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sghis.Core.Entity.Base;
using System.Security.Principal;

namespace Sghis.Core.Entity.Base
{
    public interface ICustomPrincipal : System.Security.Principal.IPrincipal
    {
        int Id { get; set; }
        string FullName { get; set; }
        string Department { get; set; }
        string Email { get; set; }
        string UserRoleDesc { get; set; }
        int UserRole { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            return false;
            //return Identity != null && Identity.IsAuthenticated &&
            //   !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role);
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string UserRoleDesc { get; set; }
        public int UserRole { get; set; }
        public string IpAddress { get; set; }
    }

    public class CustomPrincipalSerializedModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string UserRoleDesc { get; set; }
        public int UserRole { get; set; }
        public string IpAddress { get; set; }
    }
}

