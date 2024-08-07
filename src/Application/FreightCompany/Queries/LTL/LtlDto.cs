using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.LTL
{
    public class LtlDto : IMapFrom<LTL_Company>
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public string OriginState { get; set; }
        public string DestinationState { get; set; }
        public decimal PPrice1 { get; set; }
        public decimal PPrice2 { get; set; }
        public decimal PPrice3 { get; set; }
        public decimal PPrice4 { get; set; }
        public decimal PPrice5 { get; set; }
        public decimal PPrice6 { get; set; }
        public decimal PPrice7 { get; set; }
        public decimal PPrice8 { get; set; }
        public decimal PPrice9 { get; set; }
        public decimal PPrice10 { get; set; }
        public string TruckSize { get; set; }
    }
}
