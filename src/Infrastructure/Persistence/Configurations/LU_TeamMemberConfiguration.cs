using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class LU_TeamMemberConfiguration : IEntityTypeConfiguration<LU_TeamMember>
    {
        public void Configure(EntityTypeBuilder<LU_TeamMember> builder)
        {
        }
    }
}
