using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class FreightCompany : AuditableEntity, ITrackable
    {
        public FreightCompany()
        {
            CompanyContacts = new List<CompanyContact>();
            //ShipmentFreightTypes = new List<CompanyShipmentFreightTypes>();
            FTL_Companys = new List<FTL_Company>();
            LTL_Companys = new List<LTL_Company>();
            WGSCompanyWeights = new List<WGSCompanyWeights>();
            WGSCompanyMiles = new List<WGSCompanyMiles>();
            FreightCompanyCodes = new List<FreightCompanyCodes>();
            WGSAccesrails = new List<WGSAccesrails>();
            CompanyLinkedClients = new List<CompanyLinkedClients>();
            CompanyDocuments = new List<CompanyDocuments>();
            Routes = new List<Route>();
            FreightPermissions = new List<FreightPermissions>();
        }
        public long Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string ShortName { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(20)]
        public string Fax { get; set; }
        [MaxLength(200)]
        [Required]
        public string TenderEmail { get; set; }

        public bool IsActive { get; set; }

        public string Address { get; set; }
        public string Address1 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(20)]
        public string ZipCode { get; set; }
        [MaxLength(50)]
        public string Region { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(50)]
        public string PaymentMethod { get; set; }
        [MaxLength(50)]
        public string PaymentTerm { get; set; }
        public Decimal LabourCost { get; set; }

        public bool FTL { get; set; }
        public bool LTL { get; set; }
        public bool WGS { get; set; }
        [ForeignKey("Company_Id")]
        public IList<CompanyContact> CompanyContacts { get; set; }
        //[ForeignKey("Company_Id")]
        //public IList<CompanyShipmentFreightTypes> ShipmentFreightTypes { get; set; }
        [ForeignKey("Company_Id")]
        public IList<FTL_Company> FTL_Companys { get; set; }
        [ForeignKey("Company_Id")]
        public IList<LTL_Company> LTL_Companys { get; set; }
        [ForeignKey("Company_Id")]
        public IList<WGSCompanyWeights> WGSCompanyWeights { get; set; }
        [ForeignKey("Company_Id")]
        public IList<WGSCompanyMiles> WGSCompanyMiles { get; set; }
        [ForeignKey("Company_Id")]
        public IList<FreightCompanyCodes> FreightCompanyCodes { get; set; }
        [ForeignKey("Company_Id")]
        public IList<WGSAccesrails> WGSAccesrails { get; set; }
        [ForeignKey("Company_Id")]
        public IList<CompanyLinkedClients> CompanyLinkedClients { get; set; }
        [ForeignKey("Company_Id")]
        public IList<CompanyDocuments> CompanyDocuments { get; set; }
        [ForeignKey("Company_Id")]
        public IList<Route> Routes { get; set; }
        
        [ForeignKey("Company_Id")]
        public IList<FreightPermissions> FreightPermissions { get; set; }
    }
}
