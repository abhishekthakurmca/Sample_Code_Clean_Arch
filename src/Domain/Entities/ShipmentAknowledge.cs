using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class ShipmentAknowledge : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long RouteShipment_Id { get; set; }//FK of RouteShipment table
        public string RejectionToken { get; set; }
        public string ConfirmToken { get; set; }
        //public string ConfirmSecondToken { get; set; }
        //public string ConfirmThirdToken { get; set; }
        public bool IsExpired { get; set; }
        public string SentEmail { get; set; }
        public string AccountType { get; set; }
        public virtual RouteShipments _RouteShipments { get;set;}

    }
}
