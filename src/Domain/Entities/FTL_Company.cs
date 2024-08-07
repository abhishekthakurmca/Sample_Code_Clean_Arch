using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FTL_Company : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string OriginCity { get; set; }
        [Required]
        [MaxLength(100)]
        public string DestinationCity { get; set; }  
        [Required]
        [MaxLength(100)]
        public string OriginState { get; set; }
        [Required]
        [MaxLength(100)]
        public string DestinationState { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? Truck_Id { get; set; }
        public virtual LU_Truck Truck { get; set; }
    }
}
