using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Dtos.NoteDtos
{
    public class NoteDto
    {

        public required Guid Id{ get; set; }
        public required string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public required string TaskName { get; set; }
        public required string CreatedByUsername { get; set; }
        public required string CreatedByFullName { get; set; }
    }
}
