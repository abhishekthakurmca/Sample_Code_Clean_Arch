using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightCompanyCodes : AuditableEntity
    {
        public FreightCompanyCodes()
        {
            WGSAccesrails = new List<WGSAccesrails>();
            FreightInvoiceItems = new List<FreightInvoiceItems>();

        }
        public long Id { get; set; }
        public long FreightCategory_Id { get; set; }
        public long Company_Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("FreightCode_Id")]
        public IList<WGSAccesrails> WGSAccesrails { get; set; }
        [ForeignKey("FreightCode_Id")]
        public IList<FreightInvoiceItems> FreightInvoiceItems { get; set; }


    }
}
