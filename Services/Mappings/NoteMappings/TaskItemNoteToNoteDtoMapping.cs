using Shared.Dtos.NoteDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings.NoteMappings
{
    public static class TaskItemNoteToNoteDtoMapping
    {
        public static NoteDto MapTo<T>(this Domain.Entities.TaskItemNote note) where T : NoteDto
        {
            return new NoteDto
            {
                Id = note.Id,
                CreatedByFullName = note.CreatedBy.FirstName + " " + note.CreatedBy.LastName,
                CreatedByUsername = note.CreatedBy.Username,
                TaskName = note.TaskItem.Name,
                Title = note.Title,
                Content = note.Content
            };
        }
    }
}
