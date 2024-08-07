using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    
    public class FreightInvoicesConfiguration : IEntityTypeConfiguration<FreightInvoices>
    {
        public void Configure(EntityTypeBuilder<FreightInvoices> builder)
        {
            builder.Property(l => l.GrandTotal)
                .HasColumnType("decimal(18, 6)");

            builder.Property(l => l.Quote)
                .HasColumnType("decimal(18, 6)");

            builder.Property(l => l.Percentage)
                .HasColumnType("decimal(18, 3)");
        }
    }
}
