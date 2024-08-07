using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Anubis.Domain.Entities
{
  public  class WGSCompanyWeights : AuditableEntity, ITrackable
    {
        public WGSCompanyWeights()
        {
            WGSCompanyPrice = new List<WGSCompanyPrice>();
        }
        public long Id { get; set; }
        public long Company_Id { get; set; }
       
        public long From { get; set; }
        public long To { get; set; }
        public string LabelValue { get; set; }
        public bool IsDeleted { get; set; }
        //[InverseProperty("WGSCompanyPrice_")]
        //public virtual WGSCompanyPrice WGSCompanyPrice_ { get; set; }
        [ForeignKey("CompanyWGSWeight_Id")]
        public IList<WGSCompanyPrice> WGSCompanyPrice { get; set; }
    }
}
