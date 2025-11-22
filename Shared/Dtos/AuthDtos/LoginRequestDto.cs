using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.AuthDtos
{
    public class LoginRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
