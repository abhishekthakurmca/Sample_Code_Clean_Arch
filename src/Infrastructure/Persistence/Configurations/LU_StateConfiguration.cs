using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    
    public class LU_StateConfiguration : IEntityTypeConfiguration<LU_State>
    {
        public void Configure(EntityTypeBuilder<LU_State> builder)
        {
        }
    }
}
