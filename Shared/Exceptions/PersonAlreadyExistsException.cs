using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Exceptions
{
    public class PersonAlreadyExistsException : ApiException
    {
        public PersonAlreadyExistsException(string username) : base("Person Already Exists",400, $"A person with the username '{username}' already exists.")
        {
        }
    }
}
