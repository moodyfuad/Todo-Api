using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Persistant.Configurations
{
    internal sealed class TaskItemGroupConfigurations : IEntityTypeConfiguration<TaskItemGroup>
    {
        public void Configure(EntityTypeBuilder<TaskItemGroup> builder)
        {
            builder
                .HasOne(g => g.CreatedBy)
                .WithMany(p => p.CreatedGroups)
                .HasForeignKey(g => g.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
