using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class DockScheduledConfiguration : IEntityTypeConfiguration<DockScheduled>
    {
        public void Configure(EntityTypeBuilder<DockScheduled> builder)
        {
        }
    }
}
