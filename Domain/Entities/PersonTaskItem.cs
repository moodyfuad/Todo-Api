using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class PersonTaskItem : BaseEntity
    {
        public virtual Person AssignedTo { get; set; } = null!;
        public virtual TaskItem TaskItem { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(AssignedTo))]
        public virtual Guid AssignedToId { get; set; }
        [Required]
        [ForeignKey(nameof(TaskItem))]
        public virtual Guid TaskItemId { get; set; }
    }
}
