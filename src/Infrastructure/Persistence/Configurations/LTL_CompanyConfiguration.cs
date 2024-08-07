using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{

    public class LTL_CompanyConfiguration : IEntityTypeConfiguration<LTL_Company>
    {
        public void Configure(EntityTypeBuilder<LTL_Company> builder)
        {
            builder.Property(l => l.PPrice1)
                .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice2)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice3)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice4)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice5)
                .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice6)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice7)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice8)
               .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice9)
                .HasColumnType("decimal(18, 6)");
            builder.Property(l => l.PPrice10)
               .HasColumnType("decimal(18, 6)");
        }
    }
}
