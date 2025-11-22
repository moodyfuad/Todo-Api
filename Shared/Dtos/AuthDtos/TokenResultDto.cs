using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.AuthDtos
{
    public class TokenResultDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
