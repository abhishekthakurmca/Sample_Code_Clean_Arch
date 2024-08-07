using Anubis.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.GetFreightCompanies
{
  public  class FreightCompanyDto : IMapFrom<Domain.Entities.FreightCompany>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string TenderEmail { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        //public List<int> ShipmentFreightTypes { get; set; }
    }
}
