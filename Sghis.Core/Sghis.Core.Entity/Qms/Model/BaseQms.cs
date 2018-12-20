using Sghis.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Qms
{

    //add all common properties of master
    public abstract class BaseQms : BaseModel
    {
    }

    public abstract class BaseQmsGrouped : BaseModelGrouped
    {
    }

    public abstract class BaseQmsSimple
    {
        public BaseQmsSimple()
        {
            Deleted = false;
            ActionAt = DateTime.Now;
        }
        public virtual int Id { get; set; }
        public virtual DateTime ActionAt { get; set; }
        [StringLength(99)]
        public virtual string ActionBy { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
