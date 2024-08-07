using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class FreightInvoiceHeadersConfiguration : IEntityTypeConfiguration<FreightInvoiceHeaders>
    {
        public void Configure(EntityTypeBuilder<FreightInvoiceHeaders> builder)
        {
            builder.Property(l => l.FreightCharges)
                   .HasColumnType("decimal(18, 6)");


        }
    }
}
