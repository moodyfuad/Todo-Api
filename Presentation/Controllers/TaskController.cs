using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions;
using Presentation.Responses;
using Service.Abstraction;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.TaskDtos;
using Shared.Helpers;
using Shared.OperationResults;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/group/{groupId}/[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly IServiceManager _serviceManager;

        public TaskController(ILogger<TaskController> logger, IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
            _logger = logger;

        }


        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid taskId)
        {
            return Ok(ApiResponse<TaskDto>.Success(await _serviceManager.TaskServices.GetTask(taskId)));
        }
        [HttpGet("/api/Group/{groupId}/tasks")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskDto>>>> GetTasks([FromQuery] PaginationParameters parameters, Guid groupId)
        {
            PagedList<TaskDto> result = await _serviceManager.TaskServices.GetTasks(groupId, parameters);
            Response.AddPaginationHeader(result);

            return Ok(ApiResponse<IEnumerable<TaskDto>>.Success(result.Items));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> CreateTask([FromQuery] CreatedTaskDto dto, Guid groupId)
        {
            OperationResult result = await _serviceManager.TaskServices.CreateTask(groupId, dto);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }
        [HttpPut("{taskId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTask([FromQuery] UpdatedTaskDto dto,Guid taskId, Guid groupId)
        {
            OperationResult result = await _serviceManager.TaskServices.UpdateTask(taskId, dto);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }
        [HttpDelete("{taskId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTask(Guid taskId, Guid groupId)
        {
            OperationResult result = await _serviceManager.TaskServices.DeleteTask(groupId, taskId);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }
        [HttpPut("{taskId}/status")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateTaskStatus(Guid taskId, Guid groupId, TaskItemStatusDto statusDto)
        {
            //todo: add updated by user
            OperationResult result = await _serviceManager.TaskServices.UpdateTaskStatus(groupId, taskId, statusDto);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }
        [HttpGet("{taskId}/persons")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PersonDto>>>> GetTaskPersons(Guid taskId, Guid groupId)
        {
            //todo: add updated by user
            IEnumerable<PersonDto> result = await _serviceManager.TaskServices.GetTaskMembers(groupId, taskId);

            return Ok(ApiResponse < IEnumerable < PersonDto >>.Success(result));
        }

        [HttpPost("{taskId}/person/{personId}")]
        public async Task<ActionResult<ApiResponse<bool>>> AddPersonToTask(Guid taskId, Guid groupId, Guid personId)
        {
            //todo: add updated by user
            OperationResult result = await _serviceManager.TaskServices.AddPersonToTask(groupId, taskId, personId);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }
        [HttpDelete("{taskId}/person/{personId}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemovePersonToTask(Guid taskId, Guid groupId, Guid personId)
        {
            //todo: add updated by user
            OperationResult result = await _serviceManager.TaskServices.RemovePersonFromTask(groupId, taskId, personId);
            if (!result.IsSuccess)
            {
                BadRequest(ApiResponse<bool>.Fail(result.ErrorMessage));
            }
            return Ok(ApiResponse<bool>.Success(result.IsSuccess));
        }

    }
}
