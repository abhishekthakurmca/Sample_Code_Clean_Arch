using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
 
    public class FreightCompanyCodesConfiguration : IEntityTypeConfiguration<FreightCompanyCodes>
    {
        public void Configure(EntityTypeBuilder<FreightCompanyCodes> builder)
        {
            builder.Property(l => l.DefaultPrice)
                .HasColumnType("decimal(18, 6)");
        }
    }
}
