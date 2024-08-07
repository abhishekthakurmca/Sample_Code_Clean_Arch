using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightInvoices : AuditableEntity, ITrackable
    {
        public FreightInvoices()
        {
            FreightInvoiceHeaders = new List<FreightInvoiceHeaders>();
        }
        public long Id { get; set; }
        public long Route_Id { get; set; }
        public bool IsApproved { get; set; }
        public decimal Quote { get; set; }
        public decimal Percentage { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime ApprovedDate { get; set; }
        public virtual Route _Route { get; set; }
        [ForeignKey("FreightInvoice_Id")]
        public IList<FreightInvoiceHeaders> FreightInvoiceHeaders { get; set; }
        public bool IsDeleted { get; set; }
    }
}
