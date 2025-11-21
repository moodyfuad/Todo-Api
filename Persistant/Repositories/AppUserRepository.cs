using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Persistant.Identity;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistant.Repositories
{
    internal sealed class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            
            _userManager = userManager;
        }
        public async Task<bool> CheckUserCredentails(string emial,string password)
        {
            var user = await _userManager.FindByEmailAsync(emial);
            if (user == null) {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<RegisterResponseDto> RegisterAsync(string email, string password, string name)
        {
            var appUser = new AppUser
            {
                
                UserName = email,
                Email = email,
                Name = name
            };

            var result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)

                return new RegisterResponseDto(false, errors: result.Errors.Select(e=>e.Description).ToList());

            return new RegisterResponseDto(true);
        }
    }
}
