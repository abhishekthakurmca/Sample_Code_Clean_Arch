using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class TeamMemberShipments : AuditableEntity
    {
        public int Id { get; set; }
        public long ShipmentId { get; set; }
        [ForeignKey("LU_TeamMember")]
        public int TeamMember_Id { get; set; }
        public LU_TeamMember LU_TeamMember { get; set; }
    }
}
