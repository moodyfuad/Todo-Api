using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstraction
{
    public interface IAuth
    {
        Task<TokenResultDto> LogIn(LoginRequestDto dto);
        Task<TokenResultDto> RefreshToken(RefreshTokenRequestDto dto);

    }
}
