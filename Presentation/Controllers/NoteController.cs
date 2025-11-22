using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions;
using Presentation.Responses;
using Service.Abstraction;
using Shared.Dtos.NoteDtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.TaskDtos;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Task/{taskId}/[controller]")]
    public class NoteController : ControllerBase
    {

        private readonly ILogger<NoteController> _logger;
        private readonly IServiceManager _serviceManager;

        public NoteController(ILogger<NoteController> logger, IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
            _logger = logger;

        }


        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNote(Guid taskId,Guid noteId)
        {
            NoteDto noteDto = await _serviceManager.NoteServices.GetNote(noteId);
            return Ok(ApiResponse<NoteDto>.Success(noteDto));
        }
        [HttpGet("/api/Task/{taskId}/notes")]
        public async Task<ActionResult<ApiResponse<IEnumerable<NoteDto>>>> GetNotes([FromQuery] PaginationParameters parameters, Guid taskId)
        {
            PagedList<NoteDto> result = await _serviceManager.NoteServices.GetNotes(taskId, parameters);
            Response.AddPaginationHeader(result);

            return Ok(ApiResponse<IEnumerable<NoteDto>>.Success(result.Items));
        }
        [HttpPost()]
        public async Task<ActionResult<ApiResponse<bool>>> CreateTaskNote(Guid taskId, CreatedUpdatedNoteDto dto)
        {
            bool created = await _serviceManager.NoteServices.CreateNote(taskId, dto);
            if (created)
            {
                Response.StatusCode = (int) HttpStatusCode.Created;
            }
            return ApiResponse<bool>.Success();
        }
        [HttpPut("{noteId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTaskNote(Guid taskId, Guid noteId, CreatedUpdatedNoteDto dto)
        {
            bool result = await _serviceManager.NoteServices.UpdateNote(taskId, noteId, dto);
            if (result)
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            return Ok(ApiResponse<bool>.Success());
        }
        [HttpDelete("{noteId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTaskNote(Guid taskId, Guid noteId, Guid actionBy)
        {
            bool result = await _serviceManager.NoteServices.DeleteNote(taskId, noteId, actionBy);
            if (!result)
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return ApiResponse<bool>.Fail("Can not delete this note");
            }
            
            
            Response.StatusCode = (int)HttpStatusCode.NoContent;
            return ApiResponse<bool>.Success(result);
        }
    }
}
