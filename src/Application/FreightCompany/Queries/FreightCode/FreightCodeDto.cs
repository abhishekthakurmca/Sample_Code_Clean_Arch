using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.FreightCode
{
    public class FreightCodeDto : IMapFrom<FreightCompanyCodes>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal DefaultPrice { get; set; }
    }
}
