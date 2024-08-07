using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.FreightCompany.Queries.LinkedClients
{
    public class CompanyLinkedClientDto : IMapFrom<CompanyLinkedClients>
    {
        public long Id { get; set; }
        public string ClientName { get; set; }
        public string FreightType { get; set; }
        public string CollectionName { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSecondary { get; set; }
        public bool ApprovalRequired { get; set; }
    }
}
