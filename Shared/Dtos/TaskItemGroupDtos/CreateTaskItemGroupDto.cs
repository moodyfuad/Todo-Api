using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Dtos.TaskItemGroupDtos
{
    public class CreateTaskItemGroupDto
    {
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }

    }
}
