using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{

    public class WGSCompanyPriceConfiguration : IEntityTypeConfiguration<WGSCompanyPrice>
    {
        public void Configure(EntityTypeBuilder<WGSCompanyPrice> builder)
        {
            builder.Property(l => l.Price)
                .HasColumnType("decimal(18, 6)");
        }
    }
}
