using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Arms
{

    //add all common properties of master
    public abstract class BaseArms : BaseModel
    {
    }

    public abstract class BaseArmsGrouped : BaseModelGrouped
    {
    }

    public abstract class BaseArmsSimple
    {
        public BaseArmsSimple()
        {
            Deleted = false;
            ModifiedAt = DateTime.Now;
        }
        public virtual int Id { get; set; }
        public virtual DateTime? ModifiedAt { get; set; }
        [StringLength(99)]
        public virtual string ModifiedBy { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
