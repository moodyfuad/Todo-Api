using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = "The requested resource was not found.")
            : base(message, statusCode: 404) { }
    }
}
