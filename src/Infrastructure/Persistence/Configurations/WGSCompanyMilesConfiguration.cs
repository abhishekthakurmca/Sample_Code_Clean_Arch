using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{

    public class WGSCompanyMilesConfiguration : IEntityTypeConfiguration<WGSCompanyMiles>
    {
        public void Configure(EntityTypeBuilder<WGSCompanyMiles> entity)
        {


            //entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.LabelValue).IsUnicode(false);
            entity.Property(l => l.Price)
                            .HasColumnType("decimal(18, 6)");


            //entity.HasOne(d => d.WGSCompanyPrice_)
            //    .WithMany()
            //    .HasForeignKey(d => d.Id)
            //    .OnDelete(DeleteBehavior.)
            //    .HasConstraintName("FK_WGSCompanyMiles_Carrier");
        }
    }
}
