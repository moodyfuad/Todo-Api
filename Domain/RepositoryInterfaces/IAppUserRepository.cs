using Microsoft.Win32;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IAppUserRepository
    {
        Task<RegisterResponseDto> RegisterAsync(string email, string password, string name);
        Task<bool> CheckUserCredentails(string emial, string password);
    }
}
