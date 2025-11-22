using Domain.Entities;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings.PersonMappings
{
    public static class PersonDtoToPersonMapping
    {
        public static Person MapTo<TDestination>(this PersonDto source)
          where TDestination : Person
        {
            ArgumentNullException.ThrowIfNull(source);

            var destination = new Person
            {
                Id = source.Id,
                Username = source.Username,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Password = source.Password

            };
            return destination;
        }
    }
}


