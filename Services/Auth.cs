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
        private readonly IJwtService _JwtService;

        public Auth(IRepositoryManager repos, IJwtService jwtService)
        {
            _repos = repos;
            _JwtService = jwtService;
        }

        public async Task<TokenResultDto> LogIn(LoginRequestDto dto)
        {
            try
            {
                var user = await _repos.GetGenericRepository<Person>().FindAsync(
                    u => u.Username == dto.Username && u.Password == dto.Password,
                    trackChanges: true
                    );
                TokenResultDto tokenResult = new()
                {
                    AccessToken = _JwtService.GenerateAccessToken(user.Id, user.Username, user.Roles.Select(r => r.ToString()).ToList()),
                    RefreshToken = _JwtService.GenerateRefreshToken()
                };
                user.RefreshToken = tokenResult.RefreshToken;
                await _repos.UnitOfWork.SaveChangesAsync();
                return tokenResult;

            }
            catch (Exception _)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }; 
        }

        public async Task<TokenResultDto> RefreshToken(RefreshTokenRequestDto dto)
        {
            var user = await _repos.GetGenericRepository<Person>().FindAsync(
                   u => u.RefreshToken == dto.RefreshToken,
                   trackChanges: true);
            TokenResultDto tokenResult = new()
            {
                AccessToken = _JwtService.GenerateAccessToken(user.Id, user.Username, user.Roles.Select(r => r.ToString()).ToList()),
                RefreshToken = _JwtService.GenerateRefreshToken()
            };
            user.RefreshToken = tokenResult.RefreshToken;
            await _repos.UnitOfWork.SaveChangesAsync();

            return tokenResult;
        }
    }
}
