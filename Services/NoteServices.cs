using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Service.Abstraction;
using Services.Mappings.NoteMappings;
using Shared.Dtos.NoteDtos;
using Shared.Dtos.Pagination;
using Shared.Exceptions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class NoteServices : INoteServices
    {
        private readonly IRepositoryManager _repos;

        public NoteServices(IRepositoryManager repositoryManager)
        {
            _repos = repositoryManager;
        }

        public async Task<bool> CreateNote(Guid taskId, CreatedUpdatedNoteDto dto)
        {
            TaskItem task = await _repos.GetGenericRepository<TaskItem>().GetByIdAsync(taskId);
            await _repos.GetGenericRepository<TaskItemNote>().AddAsync(dto.MapTo<CreatedUpdatedNoteDto>(task));
            await _repos.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNote(Guid taskId, Guid noteId, Guid actionById)
        {
            TaskItemNote note = await _repos.GetGenericRepository<TaskItemNote>().
                FindAsync(
                n => n.Id == noteId && n.TaskItemId == taskId,
                includes: [n => n.TaskItem, n => n.TaskItem.TaskGroup, n => n.TaskItem.TaskGroup.PersonGroups, n => n.CreatedBy]
                );
            if (note.CreatedById != actionById)
            {
                var personGroup = note.TaskItem.TaskGroup.PersonGroups.FirstOrDefault(pg => pg.PersonId == actionById);
                if (personGroup is null || personGroup.PersonRole != UserRoles.Admin)
                {
                    throw new AccessForbiddenException("you can not delete this note");
                }
            }
            _repos.GetGenericRepository<TaskItemNote>().Remove(note);
            await _repos.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<NoteDto> GetNote(Guid noteId)
        {
            TaskItemNote note = await _repos.GetGenericRepository<TaskItemNote>().
                FindAsync(
                n => n.Id == noteId,
                includes: [n => n.TaskItem, n => n.CreatedBy]
                );
            return note.MapTo<NoteDto>();
        }

        public async Task<PagedList<NoteDto>> GetNotes(Guid taskId, PaginationParameters parameters)
        {
            var notes = await _repos.GetGenericRepository<TaskItemNote>()
                .GetPagedAsync(
                    parameters.PageNumber,
                    parameters.PageSize,
                    n => n.TaskItemId == taskId,
                    includes: [n => n.TaskItem, n => n.CreatedBy]
                );
            return notes.Map(n => n.MapTo<NoteDto>());
        }

        public async Task<bool> UpdateNote(Guid taskId, Guid noteId, CreatedUpdatedNoteDto dto)
        {
            TaskItemNote note = await _repos.GetGenericRepository<TaskItemNote>().
               FindAsync(
                n => n.Id == noteId && n.TaskItemId == taskId && n.CreatedById == dto.ActionBy,
                trackChanges: true);

            note.Title = dto.Title;
            note.Content = dto.Content;

            await _repos.UnitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
