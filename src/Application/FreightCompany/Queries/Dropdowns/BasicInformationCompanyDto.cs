using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.Dropdowns
{
    public class BasicInformationCompanyDto
    {
        public List<LU_Country> Countries { get; set; }
        public List<LU_State> States { get; set; }
        //public List<LU_ShipmentFreightTypes> ShipmentFreightTypes { get; set; }
        //public List<MultiCheckboxShipment> MultiCheckboxShipmens { get; set; }
        public List<LU_ClientContactTypes> ClientContactTypes { get; set; }
        public List<Payment> PaymentMethods { get; set; }
        public List<Payment> PaymentTerms { get; set; }
        public List<FreightCategory> FreightCategories { get; set; }
        public List<FreightCompanyCodes> FreightCompanyCodes { get; set; }
    }
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //public class MultiCheckboxShipment
    //{
    //    public int Id { get; set; }
    //    public bool IsSelected { get; set; }
    //    public string Name { get; set; }
    //}
}
