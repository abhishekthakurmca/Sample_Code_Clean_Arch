using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
  public  class LU_ClientContactTypes : AuditableEntity, ITrackable
    {
        //public LU_ClientContactTypes()
        //{
        //    CompanyShipmentFreightTypes = new List<CompanyShipmentFreightTypes>();

        //}
        public int Id { get; set; }
        public string Name { get; set; }
        //[ForeignKey("ShipmentFreight_Id")]
        //public IList<CompanyShipmentFreightTypes> CompanyShipmentFreightTypes { get; set; }
       
        [ForeignKey("ContactType_Id")]
        public IList<CompanyContact> CompanyContacts { get; set; }
    }
}
