using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.AuthDtos
{
    public class RefreshTokenRequestDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
