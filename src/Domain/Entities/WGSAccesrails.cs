using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class WGSAccesrails : AuditableEntity
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long FreightCode_Id { get; set; }
        public bool IsFixedPrice { get; set; }
        public bool IsDeleted { get; set; }
        public decimal DefaultPrice { get; set; }
        public virtual FreightCompanyCodes FreightCompanyCodes { get; set; }
    }
}
