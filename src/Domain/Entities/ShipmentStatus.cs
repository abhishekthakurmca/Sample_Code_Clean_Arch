using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class ShipmentStatus : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long RouteShipment_Id { get; set; }//FK of RouteShipment table
        //public long ShipmentId { get; set; }
        //public long RouteId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }

    }
}
