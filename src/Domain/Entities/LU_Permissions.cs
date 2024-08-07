using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
  public  class LU_Permissions : AuditableEntity
    {
        public LU_Permissions()
        {
            FreightPermissions = new List<FreightPermissions>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool FreightPermision { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Permission_Id")]
        public IList<FreightPermissions> FreightPermissions { get; set; }
    }
}
