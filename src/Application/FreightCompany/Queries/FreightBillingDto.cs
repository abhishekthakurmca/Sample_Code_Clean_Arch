using System;

namespace Anubis.Application.FreightCompany.Queries
{
    public class FreightBillingDto
    {
        public long Route_Id { get; set; }
        public string ShipmentId { get; set; }
        public string CompanyName { get; set; }
        public string ClientName { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string CollectionPoint { get; set; }
        public string SiteName { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Percentage { get; set; }
        public decimal Quote { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string CollectionPointToolTip { get; set; }
        public string SiteNameToolTip { get; set; }
        public string InvoiceNumberToolTip { get; set; }
        public string InvoiceDateToolTip { get; set; }
        public string ClientNameToolTip { get; set; }
        public string StateToolTip { get; set; }
        public string CityToolTip { get; set; }
        public string ShipmentIdToolTip { get; set; }
    }
}
