using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
   public class FTL_CompanyConfiguration : IEntityTypeConfiguration<FTL_Company>
    {
        public void Configure(EntityTypeBuilder<FTL_Company> builder)
        {
            builder.Property(l => l.Price)
                .HasColumnType("decimal(18, 6)");
        }
    }
}
