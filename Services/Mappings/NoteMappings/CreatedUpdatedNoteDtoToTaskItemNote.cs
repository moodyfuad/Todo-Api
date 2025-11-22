using Domain.Entities;
using Shared.Dtos.NoteDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings.NoteMappings
{
    public static class CreatedUpdatedNoteDtoToTaskItemNote
    {
        public static TaskItemNote MapTo<T>(this CreatedUpdatedNoteDto dto, TaskItem task) where T : CreatedUpdatedNoteDto
        {
            return new TaskItemNote
            {
                Title = dto.Title,
                Content = dto.Content,
                TaskItemId = task.Id,
                CreatedById = dto.ActionBy
                
            };
        }       
    }
}
