using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions;
using Presentation.Responses;
using Service.Abstraction;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.PersonDtos;
using Shared.Dtos.TaskItemGroupDtos;
using Shared.Helpers;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;
        private readonly IServiceManager _serviceManager;

        public PersonController(ILogger<PersonController> logger,IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
            _logger = logger;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PersonDto>>> Get(Guid id)
        {
            return Ok(ApiResponse<PersonDto>.Success(await _serviceManager.PersonServices.GetPersonById(id)));
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> Addperson([FromForm] PersonDto personDto)
        {
            bool success = (await _serviceManager.PersonServices.CreatePerson(personDto)) != null;
            return ApiResponse<bool>.Success(success);
        }

        [HttpGet("/api/Persons")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PersonDto>>>> GetPersons()
        {
            var result = await _serviceManager.PersonServices.GetPersons(new PersonSearchParameters());
            Response.AddPaginationHeader(result);
            return new ApiResponse<IEnumerable<PersonDto>>(result.Items);
        }
        [HttpGet("/api/Persons/search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PersonDto>>>> GetPersons([FromQuery] PersonSearchParameters parameters)
        {
            var result = await _serviceManager.PersonServices.GetPersons(parameters);
            Response.AddPaginationHeader(result);
            return new ApiResponse<IEnumerable<PersonDto>>(result.Items);
        }
       
        [HttpGet("{id}/groups")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PersonGroupDto>>>> GetUserGroups([FromQuery] PaginationParameters parameters, Guid id)
        {
            var result = await _serviceManager.PersonGroupServices.GetUserGroups(id, parameters);
            Response.AddPaginationHeader(result);
            return Ok(ApiResponse<IEnumerable<PersonGroupDto>>.Success(result.Items));
        }

        [HttpPost("{id}/group")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> CreateGroup(Guid id, [FromBody] CreateTaskItemGroupDto dto)
        {
            var userId = User.GetUserId();

            var result = await _serviceManager.PersonGroupServices.CreateGroup(id, dto);
            if (result.IsSuccess)
            {
                return Created();
            }
            return BadRequest(ApiResponse<bool>.Fail("Failed to create group", errors: result.ErrorMessage));
        }

    }
}
