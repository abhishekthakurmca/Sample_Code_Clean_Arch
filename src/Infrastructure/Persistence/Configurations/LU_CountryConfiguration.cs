using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
   
    public class LU_CountryConfiguration : IEntityTypeConfiguration<LU_Country>
    {
        public void Configure(EntityTypeBuilder<LU_Country> builder)
        {
        }
    }
}
