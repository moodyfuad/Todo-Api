using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Persistant.Configurations
{
    internal sealed class TaskItemConfigurations : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {

            builder
                .HasOne(t => t.CreatedBy)
                .WithMany(p => p.CreatedTasks)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.EditedBy)
                .WithMany(p => p.EditedTasks)
                .HasForeignKey(t => t.EditedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(t => t.TaskGroup)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t => t.TaskGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
