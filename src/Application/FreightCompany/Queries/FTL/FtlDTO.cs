using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
   public class FtlDTO : IMapFrom<FTL_Company>
    {
        public long Id { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public string OriginState { get; set; }
        public string DestinationState { get; set; }
        public decimal Price { get; set; }
        public string TruckSize { get; set; }
    }
}
