using Anubis.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.GetFreightCompanies
{
  
    public class FreightCompanyEditDto : IMapFrom<Domain.Entities.FreightCompany>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public string Address { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTerm { get; set; }
        //public List<int> ShipmentFreightTypes { get; set; }
        public bool FTL { get; set; }
        public bool LTL { get; set; }
        public bool WGS { get; set; }
        public bool IsActive { get; set; }
        //public List<MultiCheckboxShipment> MultiCheckboxShipments { get; set; }
        public string TenderEmail { get; set; }
    }
    //public class MultiCheckboxShipment
    //{
    //    public int Id { get; set; }
    //    public bool IsSelected { get; set; }
    //    public string Name { get; set; }
    //}
}
