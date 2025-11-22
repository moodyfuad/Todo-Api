using Domain.Entities;
using Shared.Dtos;
using Shared.Dtos.TaskDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings.TaskItemMappings
{
    public static class TaskItemToTaskDtoMapping
    {
        public static TaskDto MapTo<T>(this TaskItem task) where T : TaskDto
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = (TaskItemStatusDto)task.Status,
                CreatedAt = task.CreatedAt,
                CreatedByUsername = task.CreatedBy.Username,
                EditedByUsername = task.EditedBy != null ? task.EditedBy.Username : string.Empty,
                NotesCount = task.Notes.Count,
                UpdatedAt = task.UpdatedAt

            };
        }
    }
}
