using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Service.Abstraction
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId, string usernmae, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Guid GetUserIdFromToken(string token);
    }

}
