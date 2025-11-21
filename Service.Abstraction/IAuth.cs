using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstraction
{
    public interface IAuth
    {
        Task<string> LogIn(string username, string password);
        Task<RegisterResponseDto> RegisterAsync(string email, string password, string name);

    }
}
