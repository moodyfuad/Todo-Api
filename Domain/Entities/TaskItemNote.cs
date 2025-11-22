using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TaskItemNote : BaseEntity
    {
        [Required]
        public required string Title { get; set; }
        public string Content { get; set; } = string.Empty;

        public virtual TaskItem TaskItem { get; set; }

        [Required]
        [ForeignKey(nameof(TaskItem))]
        public required Guid TaskItemId { get; set; }

        public virtual Person CreatedBy { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedBy))]
        public required Guid CreatedById { get; set; }

    
    }
}
