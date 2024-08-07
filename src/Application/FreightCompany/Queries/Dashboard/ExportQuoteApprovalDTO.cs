using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.Dashboard
{
    public class ExportQuoteApprovalDTO : IMapFrom<RouteShipments>
    {
        public long ShipmentId { get; set; }
        public long? Route_Id { get; set; }
        public string ClientName { get; set; }
        public int AgingDays { get; set; }
        public DateTime? OrderCreateDate { get; set; }
        public DateTime? QuoteApprovalDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DockDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ERISite { get; set; }
        public bool InOut { get; set; }
        public bool CustomerArranged { get; set; }
        public string ReceivingType { get; set; }
        public string FormType { get; set; }
        public int PlannedQty { get; set; }
        public string FreightCompany { get; set; }
        public decimal Quote { get; set; }
        public decimal NewQuote { get; set; }
        public string TeamMemberName { get; set; }
    }
}
