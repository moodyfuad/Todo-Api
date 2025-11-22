using Shared.Dtos.NoteDtos;
using Shared.Dtos.Pagination;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstraction
{
    public interface INoteServices
    {
        Task<bool> CreateNote(Guid taskId, CreatedUpdatedNoteDto dto);
        Task<bool> DeleteNote(Guid taskId, Guid noteId, Guid actionBy);
        Task<NoteDto> GetNote(Guid noteId);
        Task<PagedList<NoteDto>> GetNotes(Guid taskId, PaginationParameters parameters);
        Task<bool> UpdateNote(Guid taskId, Guid noteId, CreatedUpdatedNoteDto dto);
    }
}
