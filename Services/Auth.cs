using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Service.Abstraction;
using Services.JwtServices;
using Shared.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class Auth : IAuth
    {
        private readonly IRepositoryManager _repos;
        private readonly JwtHander _jwtHander;

        public Auth(IRepositoryManager repos, JwtHander jwtHander)
        {
            _repos = repos;
            _jwtHander = jwtHander;
        }

        public async Task<string> LogIn(string username, string password)
        {
            bool exist = await _repos.AppUserRepository.CheckUserCredentails(username, password);
            if (!exist) return null;
            var person = new Person() {Name = username,Role = "Admin" };
            return _jwtHander.CreateToken(person);
        }

        public async Task<RegisterResponseDto> RegisterAsync(string email, string password, string name)
        {
           return await _repos.AppUserRepository.RegisterAsync(email, password, name);
        }
    }
}
