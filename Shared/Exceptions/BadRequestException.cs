using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message = "Bad Request Exception", object? errors = null) : base(message,400,errors)
        {
        }
    }

}
