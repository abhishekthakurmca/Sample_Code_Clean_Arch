using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    public class WGSMilesDto : IMapFrom<WGSCompanyMiles>
    {
        public long Id { get; set; }
        public string LabelValue { get; set; }
        public string Miles { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public decimal MinimumCharge { get; set; }
        public string TruckSize { get; set; }
        public decimal Price { get; set; }
    }
}
