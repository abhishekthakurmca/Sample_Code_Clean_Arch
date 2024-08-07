using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class FreightInvoiceItemsConfiguration : IEntityTypeConfiguration<FreightInvoiceItems>
    {
        public void Configure(EntityTypeBuilder<FreightInvoiceItems> builder)
        {

            builder.Property(l => l.TotalPrice)
                .HasColumnType("decimal(18, 6)");

            builder.Property(l => l.Price)
                .HasColumnType("decimal(18, 6)");

            builder.Property(l => l.Quantity)
                .HasColumnType("decimal(18, 3)");
        }
    }
}
