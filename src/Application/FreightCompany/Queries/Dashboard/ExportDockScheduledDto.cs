using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.Dashboard
{
    public class ExportDockScheduledDto : IMapFrom<DockScheduled>
    {
        public long Id { get; set; }
        public long Route_Id { get; set; }
        public int DockId { get; set; }
        public int ReceivingTypeID { get; set; }
        public int SiteId { get; set; }
    }
}
