using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class CompanyLinkedClients : AuditableEntity, ITrackable
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public long ClientId { get; set; }
        [Required]
        public string ClientName { get; set; }
        public long CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string ScheduleFrequency { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public bool ApprovalRequired { get; set; }
        public bool IsDeleted { get; set; }
        public int TransportDays { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string FreightType { get; set; }
        public virtual FreightCompany _FreightCompany { get; set; }
    }
}
