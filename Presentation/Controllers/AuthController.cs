using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;
using Service.Abstraction;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager services;

        public AuthController(IServiceManager services)
        {
            this.services = services;
        }

        [HttpPost("/Login")]

        public async Task<ApiResponse<string>> LogIn(string username, string password)
        {
            string result = await services.Auth.LogIn(username, password);
            if (string.IsNullOrEmpty( result))
            {
                return ApiResponse<string>.Fail();
            }
            return ApiResponse<string>.Success(result);
        }
        [HttpPost("/Register")]

        public async Task<ApiResponse<string>> Register(string name, string emial, string password)
        {
            var result = await services.Auth.RegisterAsync(emial, password, name);
            if (!result.Success)
            {
                return ApiResponse<string>.Fail(errors:result.Errors);
            }
            return ApiResponse<string>.Success();
        }
    }
}
