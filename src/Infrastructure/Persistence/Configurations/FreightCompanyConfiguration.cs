using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class FreightCompanyConfiguration : IEntityTypeConfiguration<FreightCompany>
    {
        public void Configure(EntityTypeBuilder<FreightCompany> builder)
        {
            builder.Property(l => l.LabourCost)
                 .HasColumnType("decimal(18, 3)");
        }
    }
}
