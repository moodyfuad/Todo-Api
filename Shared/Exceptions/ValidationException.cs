using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ValidationException : ApiException
    {
        public ValidationException(string message = "Validation failed.", object? errors = null)
            : base(message, statusCode: 400, errors: errors) { }
    }
}
