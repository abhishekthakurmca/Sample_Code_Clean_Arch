using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class DockScheduled : AuditableEntity
    {
        public long Id { get; set; }
        public long Route_Id { get; set; }
        public int DockId { get; set; }
        public string Dock { get; set; }
        public int ReceivingTypeID { get; set; }
        public int SiteId { get; set; }
        public DateTime DockDate { get; set; }
        public string DockTime { get; set; }
        public string Capacity { get; set; }
        public string Site { get; set; }
        public string ReceivingType { get; set; }

        public virtual Route _Route { get; set; }
    }
}
