using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Services.JwtServices
{
    public class JwtHander
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public JwtHander(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        public string CreateToken(Person person)
        {
            var credentials = GetSignInCreditials();
            var claims = GetClaims(person);
            var options = GenerateJwtTokenOptions(credentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(options);
        }
        
        private SigningCredentials GetSignInCreditials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["Key"]));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(Person person) {
            List<Claim> claims = [];

            claims.AddRange(
                new Claim(ClaimTypes.Role, person.Role),
                new Claim(ClaimTypes.Name, person.Name)
                // Add more if needed
                );

            return claims;
        }

        private JwtSecurityToken GenerateJwtTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            DateTime expiredAfter = DateTime.Now.AddMinutes(15);
            if (double.TryParse(_jwtSettings["ExpiredAfterMin"], out double minutes))
            {
                expiredAfter = DateTime.Now.AddMinutes(minutes);
            }

            return new JwtSecurityToken(

                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                expires: expiredAfter,
                claims: claims,
                signingCredentials: credentials
                );
                
        }
    }
}
