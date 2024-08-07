using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    
    public class LU_TruckConfiguration : IEntityTypeConfiguration<LU_Truck>
    {
        public void Configure(EntityTypeBuilder<LU_Truck> builder)
        {
        }
    }
}
