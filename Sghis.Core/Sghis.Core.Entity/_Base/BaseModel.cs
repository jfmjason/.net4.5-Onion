using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sghis.Core.Entity.Base;

namespace Sghis.Core.Entity.Base
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            Deleted = false;
            CreatedAt = DateTime.Now;
        }

        public virtual int Id { set; get; }
        public virtual DateTime CreatedAt { get; set; }
        [StringLength(99)]
        public virtual string CreatedBy { get; set; }
        public virtual int? CreatedById { set; get; }
        public virtual DateTime? ModifiedAt { get; set; }
        [StringLength(99)]
        public virtual string ModifiedBy { get; set; }
        public virtual int? ModifiedById { set; get; }
        public virtual bool Deleted { get; set; }
    }

    public abstract class BaseModelGrouped : BaseModel
    {
        //https://stackoverflow.com/questions/27969861/entity-framework-sharing-entities-across-different-dbcontexts
        public virtual int? OrganizationId { set; get; }
    }


    public class Organization : BaseModel
    {
        [StringLength(99)]
        public virtual string Code { get; set; }
        [StringLength(512)]
        public virtual string Name { get; set; }
        [StringLength(512)]
        public virtual string Address { get; set; }
        public virtual Organization ParentOrganization { set; get; }
        public int? ParentOrganizationId { set; get; }
    }
}

