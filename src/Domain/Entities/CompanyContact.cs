using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class CompanyContact : AuditableEntity
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public int ContactType_Id { get; set; }

        [MaxLength(100)]
        public string FName { get; set; }
        [MaxLength(100)]
        public string LName { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string Fax { get; set; }
        [MaxLength(20)]
        public string Ext { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }

        public string Notes { get; set; }

        //public bool? IsContractNotification { get; set; }

        //public bool? IsPrimaryContact { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        //[ForeignKey(nameof(Company_Id))]
        //FreightCompany FreightCompany { get; set; }

        //[ForeignKey(nameof(ContactType_Id))]
        //LU_ClientContactTypes ClientContactTypes { get; set; }
    }
}
