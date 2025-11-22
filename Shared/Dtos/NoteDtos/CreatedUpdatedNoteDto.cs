using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.NoteDtos
{
    public class CreatedUpdatedNoteDto
    {
        public Guid ActionBy { get; set; }
        public required string Title { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
