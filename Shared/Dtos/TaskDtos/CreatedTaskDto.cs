using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.TaskDtos
{
    public class CreatedTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid CreatedById { get; set; }
    }
}
