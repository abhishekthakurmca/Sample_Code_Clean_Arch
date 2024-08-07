using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    
    public class WGSAccesrailsConfiguration : IEntityTypeConfiguration<WGSAccesrails>
    {
        public void Configure(EntityTypeBuilder<WGSAccesrails> entity)
        {
            //entity.Property(l => l.DefaultPrice)
            //              .HasColumnType("decimal(18, 6)");

        }
    }
}
