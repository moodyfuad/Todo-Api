using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException(string message, int statusCode = 500, object? errors = null) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public int StatusCode { get; }
        public object? Errors { get; }

    }
}
