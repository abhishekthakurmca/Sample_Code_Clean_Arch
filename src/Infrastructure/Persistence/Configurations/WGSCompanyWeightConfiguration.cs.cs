using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{

    public class WGSCompanyWeightConfiguration : IEntityTypeConfiguration<WGSCompanyWeights>
    {
        public void Configure(EntityTypeBuilder<WGSCompanyWeights> entity)
        {
            //entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.LabelValue).IsUnicode(false);

          

            //entity.HasOne(d => d.WGSCompanyPrice_)
            //    .WithMany()
            //    .HasForeignKey(d => d.Id)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_WGSCompanyWeights_Carrier");
            
        }
    }
}
