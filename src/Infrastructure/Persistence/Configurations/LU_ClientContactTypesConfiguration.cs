using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
  
    public class LU_ClientContactTypesConfiguration : IEntityTypeConfiguration<LU_ClientContactTypes>
    {
        public void Configure(EntityTypeBuilder<LU_ClientContactTypes> builder)
        {
        }
    }
}
