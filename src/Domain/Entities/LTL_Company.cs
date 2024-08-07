using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class LTL_Company : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        [MaxLength(100)]
        public string OriginCity { get; set; }
        [MaxLength(100)]
        public string DestinationCity { get; set; }  
        [MaxLength(100)]
        public string OriginState { get; set; }
        [MaxLength(100)]
        public string DestinationState { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public decimal PPrice1 { get; set; }
        public decimal PPrice2 { get; set; }
        public decimal PPrice3 { get; set; }
        public decimal PPrice4 { get; set; }
        public decimal PPrice5 { get; set; }
        public decimal PPrice6 { get; set; }
        public decimal PPrice7 { get; set; }
        public decimal PPrice8 { get; set; }
        public decimal PPrice9 { get; set; }
        public decimal PPrice10 { get; set; }
        public int? Truck_Id { get; set; }
        public virtual LU_Truck Truck { get; set; }
    }
}
