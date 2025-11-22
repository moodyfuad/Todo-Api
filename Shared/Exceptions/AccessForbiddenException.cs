using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public class AccessForbiddenException : ApiException
    {
        public AccessForbiddenException(string message = "Action Forbidden")
            : base(message, statusCode: 403) { }
    }
}
