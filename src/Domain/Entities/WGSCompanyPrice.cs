using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class WGSCompanyPrice : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long CompanyWGSWeight_Id { get; set; }
        public long CompanyWGSMiles_Id { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
