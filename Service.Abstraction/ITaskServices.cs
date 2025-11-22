using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.TaskDtos;
using Shared.Helpers;
using Shared.OperationResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstraction
{
    public interface ITaskServices
    {
        Task<TaskDto> GetTask(Guid taskId);
        Task<PagedList<TaskDto>> GetTasks(Guid groupId, PaginationParameters parameters);
        Task<OperationResult> CreateTask(Guid groupId, CreatedTaskDto dto);
        Task<OperationResult> DeleteTask(Guid groupId, Guid taskId);
        Task<OperationResult> UpdateTask(Guid taskId, UpdatedTaskDto dto);
        Task<OperationResult> UpdateTaskStatus(Guid groupId, Guid taskId, TaskItemStatusDto statusDto);
        // members
        Task<IEnumerable<PersonDto>> GetTaskMembers(Guid groupId, Guid taskId);
        Task<OperationResult> AddPersonToTask(Guid groupId, Guid taskId, Guid personId);
        Task<OperationResult> RemovePersonFromTask(Guid groupId, Guid taskId, Guid personId);
    }
}
