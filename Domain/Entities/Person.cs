using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? RefreshToken { get; set; }
        public virtual ICollection<UserRoles> Roles { get; set; } = [];
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [Required]
        
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }

        [InverseProperty(nameof(PersonTaskItemGroup.Person))]
        public virtual ICollection<PersonTaskItemGroup> Groups { get; set; } = [];

        [InverseProperty(nameof(PersonTaskItem.AssignedTo))]
        public virtual ICollection<PersonTaskItem> AssignedTasks { get; set; } = [];

        [InverseProperty(nameof(TaskItem.CreatedBy))]
        public virtual ICollection<TaskItem> CreatedTasks { get; set; } = [];

        [InverseProperty(nameof(TaskItemNote.CreatedBy))]

        public virtual ICollection<TaskItemNote> CreatedNotes { get; set; } = [];

        [InverseProperty(nameof(TaskItemGroup.CreatedBy))]
        public virtual ICollection<TaskItemGroup> CreatedGroups { get; set; } = [];

        [InverseProperty(nameof(TaskItem.EditedBy))]
        public virtual ICollection<TaskItem> EditedTasks { get; set; } = [];

    }
}
