using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TaskItemGroup : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [InverseProperty(nameof(TaskItem.TaskGroup))]
        public virtual ICollection<TaskItem> Tasks { get; set; } = [];

        //public virtual ICollection<Person> Members { get; set; } = [];
        [InverseProperty(nameof(PersonTaskItemGroup.TaskItemGroup))]
        public virtual ICollection<PersonTaskItemGroup> PersonGroups { get; set; } = [];

        public virtual Person CreatedBy { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CreatedBy))]
        public required Guid CreatedById { get; set; }

    }
}
