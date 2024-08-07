using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{

    public class FreightInvoiceHeaders : AuditableEntity, ITrackable
    {
        public FreightInvoiceHeaders()
        {
            FreightInvoiceItems = new List<FreightInvoiceItems>();
        }

        public long Id { get; set; }
        public long FreightInvoice_Id { get; set; }
        public decimal FreightCharges { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Notes { get; set; }
        public long TransportingMiles { get; set; }
        public string BOINumber { get; set; }
        //public decimal TotalPrice { get; set; }
        [ForeignKey("FreightInvoiceHeader_Id")]
        public IList<FreightInvoiceItems> FreightInvoiceItems { get; set; }
        public bool IsDeleted { get; set; }
    }
}
