using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.TaskDtos
{
    public class UpdatedTaskDto
    {
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid EditedById { get; set; }
    }
}
