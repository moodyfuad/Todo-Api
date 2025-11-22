using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Presentation.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(id);
        }
    }
}
