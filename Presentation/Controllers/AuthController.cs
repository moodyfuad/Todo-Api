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

        [HttpPost("Login")]

        public async Task<ApiResponse<TokenResultDto>> LogIn(LoginRequestDto dto)
        {
            TokenResultDto result = await services.Auth.LogIn(dto);
            
            return ApiResponse<TokenResultDto>.Success(result);
        }
        [HttpPost("refresh-token")]
        public async Task<ApiResponse<TokenResultDto>> RefreshToken(RefreshTokenRequestDto dto)
        {
            var result = await services.Auth.RefreshToken(dto);
            return ApiResponse<TokenResultDto>.Success(result);
        }
    }
}
