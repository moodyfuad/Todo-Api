using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Abstraction;
using API.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Helpers;
using Presentation.Responses;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;
        private readonly IServiceManager _serviceManager;

        public PersonController(ILogger<PersonController> logger,IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
            _logger = logger;

        }


        [HttpGet]
        public async Task<ActionResult<PersonDto>> Get(Guid id)
        {
            return Ok(ApiResponse<PersonDto>.Success(await _serviceManager.PersonServices.GetPersonById(id)));
        }
        [HttpGet("s")]
        public async Task<ActionResult<PagedResponse<PersonDto>>> GetPersons()
        {
            return new PagedResponse<PersonDto>(await _serviceManager.PersonServices.GetPersons(1,10));
        }
        [HttpPost]
        public async Task<bool> Addperson([FromForm] PersonDto personDto)
        {
            return (await _serviceManager.PersonServices.CreatePerson(personDto)) != null;
        }
    }
}
