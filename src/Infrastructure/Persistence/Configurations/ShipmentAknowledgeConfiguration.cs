﻿using Anubis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Infrastructure.Persistence.Configurations
{
    public class ShipmentAknowledgeConfiguration : IEntityTypeConfiguration<ShipmentAknowledge>
    {
        public void Configure(EntityTypeBuilder<ShipmentAknowledge> builder)
        {
        }
    }
}
