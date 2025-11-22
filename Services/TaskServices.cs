using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Service.Abstraction;
using Services.Mappings.PersonMappings;
using Services.Mappings.TaskItemMappings;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.TaskDtos;
using Shared.Helpers;
using Shared.OperationResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class TaskServices : ITaskServices
    {
        private readonly IRepositoryManager _repos;

        public TaskServices(IRepositoryManager repos)
        {
            this._repos = repos;
        }

        public async Task<OperationResult> AddPersonToTask(Guid groupId, Guid taskId, Guid personId)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>().
                FindAsync(
                t=> t.Id == taskId,
                includes: [t => t.PersonTasks]);
            Person person = await _repos.GetGenericRepository<Person>().
                FindAsync(
                p=> p.Id == personId,
                includes: [p => p.AssignedTasks]);

            if (task.PersonTasks.Select(t => t.AssignedToId).Contains(person.Id))
            {
                return OperationResult.Success();
            }

            PersonTaskItem personTask = new()
            {
                TaskItemId = task.Id,
                AssignedToId = person.Id
                
            };
            await _repos.GetGenericRepository<PersonTaskItem>().AddAsync(personTask);
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> CreateTask(Guid groupId, CreatedTaskDto dto)
        {
            var group = await _repos.GetGenericRepository<TaskItemGroup>().
                GetByIdAsync(
                groupId,
                includes: [tg => tg.Tasks]);
            if (group == null)
            {
                return OperationResult.Failure("Group not found");
            }
            TaskItem task = new()
            {
                Name = dto.Title,
                Description = dto.Description,
                TaskGroupId = groupId,
                CreatedAt = DateTime.UtcNow,
                CreatedById = dto.CreatedById,
                Status = TaskItemStatus.None
            };
            await _repos.GetGenericRepository<TaskItem>().AddAsync(task);
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();

        }

        public async Task<OperationResult> DeleteTask(Guid groupId, Guid taskId)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>().GetByIdAsync(taskId);
            _repos.GetGenericRepository<TaskItem>().Remove(task);
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<TaskDto> GetTask(Guid taskId)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>()
                .GetByIdAsync(
                taskId,
                includes: [t => t.CreatedBy, t => t.EditedBy, t => t.Notes]);
            return task.MapTo<TaskDto>();
        }

        public async Task<IEnumerable<PersonDto>> GetTaskMembers(Guid groupId, Guid taskId)
        {
            IEnumerable<PersonTaskItem> personTasks = await _repos.GetGenericRepository<PersonTaskItem>().
                FindRangeAsync(pt => pt.TaskItemId == taskId, includes: [pt => pt.AssignedTo]);

            return personTasks.Select(pt => pt.AssignedTo.MapTo<PersonDto>());
        }

        public async Task<PagedList<TaskDto>> GetTasks(Guid groupId, PaginationParameters parameters)
        {
           var result = await _repos.GetGenericRepository<TaskItem>()
                .GetPagedAsync(
                parameters.PageNumber,
                parameters.PageSize,
                predicate: t => t.TaskGroupId == groupId,
                includes: [t => t.CreatedBy, t => t.EditedBy, t => t.Notes]);

            return result.Map(t => t.MapTo<TaskDto>());
        }

        public async Task<OperationResult> RemovePersonFromTask(Guid groupId, Guid taskId, Guid personId)
        {
            PersonTaskItem personTask = await _repos.GetGenericRepository<PersonTaskItem>().
                FindAsync(
                pt => pt.TaskItemId == taskId && pt.AssignedToId == personId,
                includes: [pt=> pt.AssignedTo, pt => pt.TaskItem]);
            
            _repos.GetGenericRepository<PersonTaskItem>().Remove(personTask);
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<OperationResult> UpdateTask(Guid taskId, UpdatedTaskDto dto)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>().GetByIdAsync(taskId);
            task.Name = dto.Title;
            task.Description = dto.Description;
            task.EditedById = dto.EditedById;
            
            _repos.GetGenericRepository<TaskItem>().Update(task);
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();

        }

        public async Task<OperationResult> UpdateTaskStatus(Guid groupId, Guid taskId, TaskItemStatusDto statusDto)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>()
                .FindAsync(
                t=>t.Id == taskId,
                trackChanges:true);
            task.Status = (TaskItemStatus)statusDto;
            await _repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
