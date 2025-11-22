using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public virtual ICollection<PersonTaskItem> PersonTasks { get; set; } = [];
        public virtual ICollection<TaskItemNote> Notes { get; set; } = [];

        public virtual TaskItemGroup TaskGroup { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(TaskGroup))]
        public required Guid TaskGroupId { get; set; }
        
        public virtual Person CreatedBy { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CreatedBy))]
        public required Guid CreatedById { get; set; }

        public virtual Person? EditedBy { get; set; } = null;

        [ForeignKey(nameof(EditedBy))]
        public Guid? EditedById { get; set; } = null;

    }
}
