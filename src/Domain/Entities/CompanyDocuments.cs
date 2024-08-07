using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
   public class CompanyDocuments : AuditableEntity
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentName { get; set; }
        public bool IsDeleted { get; set; }

    }
}
