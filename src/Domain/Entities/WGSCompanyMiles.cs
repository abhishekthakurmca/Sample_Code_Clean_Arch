using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class WGSCompanyMiles : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long From { get; set; }
        public decimal Price { get; set; }
        public long To { get; set; }
        public string LabelValue { get; set; }
        public bool IsDeleted { get; set; }
        public int? Truck_Id { get; set; }
        public virtual LU_Truck Truck { get; set; }
    }
}
