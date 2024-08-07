using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class Route : AuditableEntity
    {
        public Route()
        {
            RouteShipments = new List<RouteShipments>();
            DockScheduled = new List<DockScheduled>();
            FreightInvoices = new List<FreightInvoices>();
        }
        public long Id { get; set; }
        public string DriverName { get; set; }
        public string FreightCompanyName { get; set; }
        public string TruckNumber { get; set; }
        public string DriverID { get; set; }
        public DateTime? PickUpDate { get; set; }
        public string PickUpTime { get; set; }

        public DateTime? DockDate { get; set; }
        public string DockTime { get; set; }
        public int DockID { get; set; }
        public string Dock { get; set; }
        public int TruckSize { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Quote { get; set; }
        public decimal NewQuote { get; set; }
        public string TrailerNumber { get; set; }
        public string SealNumber { get; set; }
        public long Company_Id { get; set; }
        [ForeignKey("Route_Id")]
        public IList<RouteShipments> RouteShipments { get; set; }
        public virtual FreightCompany _Company { get; set; }
        [ForeignKey("Route_Id")]
        public IList<DockScheduled> DockScheduled { get; set; }
        [ForeignKey("Route_Id")]
        public IList<FreightInvoices> FreightInvoices { get; set; }

    }
}
