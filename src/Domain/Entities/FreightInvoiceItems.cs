using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightInvoiceItems : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long FreightInvoiceHeader_Id { get; set; }
        public long FreightCode_Id { get; set; }
        public string FreightCode { get; set; }
        public string FreightDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
