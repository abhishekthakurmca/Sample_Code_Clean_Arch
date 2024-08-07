using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.Dashboard
{
   public class ExportShipmentDTO : IMapFrom<RouteShipments>
    {
        public long ShipmentId { get; set; }
        public string Client { get; set; }
        public string CollectionPointAddress { get; set; }
        public string CollectionPointCity { get; set; }
        public string CollectionPointState { get; set; }
        public string CollectionPointZip { get; set; }
        public string FreightType { get; set; }
        public string ShipmentProduct { get; set; }
        public string Site { get; set; }
        public string ShipmentStatus { get; set; }
        public DateTime? TenderPendingApprovalDate { get; set; }
        public bool IsInbound { get; set; }
        public bool IsArrangedByCustomer { get; set; }
    }
}
