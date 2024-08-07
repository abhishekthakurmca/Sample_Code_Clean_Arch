using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightPermissions : AuditableEntity
    {
        public long Id { get; set; }

        public long Company_Id { get; set; }
        public int Permission_Id { get; set; }
        public bool IsActive { get; set; }
        //public virtual LU_Permissions _Permissions { get;set;}
}
}
