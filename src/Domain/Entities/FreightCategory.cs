using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightCategory : AuditableEntity
    {
        public FreightCategory()
        {
            FreightCompanyCodes = new List<FreightCompanyCodes>();

        }
        public long Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("FreightCategory_Id")]
        public IList<FreightCompanyCodes> FreightCompanyCodes { get; set; }
    }
}
