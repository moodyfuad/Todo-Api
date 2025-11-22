using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public class EntityNotFoundException : ApiException
    {
        public EntityNotFoundException(string entityName)
            : base($"The {entityName} was not found.", statusCode: 404) { }
    }
}
