using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class LU_Truck : AuditableEntity
    {
        public LU_Truck()
        {
            FTL_Companies = new List<FTL_Company>();
            LTL_Companies = new List<LTL_Company>();
            WGS_CompanyMiles = new List<WGSCompanyMiles>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [ForeignKey("Truck_Id")]
        public IList<FTL_Company> FTL_Companies { get; set; }
        [ForeignKey("Truck_Id")]
        public IList<LTL_Company> LTL_Companies { get; set; }
        [ForeignKey("Truck_Id")]
        public  IList<WGSCompanyMiles> WGS_CompanyMiles { get; set; }
    }
}
