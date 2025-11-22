using Domain.Entities;
using Shared.Dtos.PersonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings.GroupMappings
{
    public static class TaskItemGroupToPersonGroupDto
    {
        public static PersonGroupDto MapTo<T>(this TaskItemGroup taskItemGroup) where T : PersonGroupDto
        {
            if (taskItemGroup == null) return null;
            return new PersonGroupDto
            {
                Id = taskItemGroup.Id,
                Name = taskItemGroup.Name,
                Description = taskItemGroup.Description??string.Empty,
                CreatorName = taskItemGroup.CreatedBy != null ? $"{taskItemGroup.CreatedBy.FirstName} {taskItemGroup.CreatedBy.LastName}" : "Unknown"
            };
        }
    }
}
