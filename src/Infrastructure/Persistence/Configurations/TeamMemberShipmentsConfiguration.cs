using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class TeamMemberShipmentsConfiguration : IEntityTypeConfiguration<TeamMemberShipments>
    {
        public void Configure(EntityTypeBuilder<TeamMemberShipments> builder)
        {
        }
    }
}
