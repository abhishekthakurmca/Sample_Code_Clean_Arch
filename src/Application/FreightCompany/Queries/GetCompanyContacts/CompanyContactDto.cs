using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.GetCompanyContacts
{
    public class CompanyContactDto : IMapFrom<CompanyContact>
    {
        public long Id { get; set; }

        public long Company_Id { get; set; }
        public int ContactType_Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Ext { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        //public int? CreatedByID { get; set; }
        //public bool? IsContractNotification { get; set; }
        //public bool? IsPrimaryContact { get; set; }
        //public bool? IsDeleted { get; set; }
        //public bool? IsChannelSalesRep { get; set; }
    }
}
