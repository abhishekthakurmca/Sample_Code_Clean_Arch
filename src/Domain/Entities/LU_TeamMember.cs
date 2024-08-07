using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class LU_TeamMember : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
