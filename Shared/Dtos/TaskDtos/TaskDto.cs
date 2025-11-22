using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Dtos.TaskDtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public TaskItemStatusDto Status { get; set; } = TaskItemStatusDto.None;

        public required int NotesCount { get; set; } = 0;

        public required string CreatedByUsername { get; set; }

        public required string EditedByUsername { get; set; } = string.Empty;

        public required DateTime CreatedAt { get; set; }
        public required DateTime? UpdatedAt { get; set; }
    }
}
