using Anubis.Application.Common.Mappings;
using Anubis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Application.ExportFiles.FreightProfiles.Category
{
   public class FreightCategoryDto : IMapFrom<FreightCategory>
    {
        public long Id { get; set; }
        public string Name { get; set; }

    }
}
