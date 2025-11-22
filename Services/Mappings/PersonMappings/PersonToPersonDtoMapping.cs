using Domain.Entities;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappings.PersonMappings
{
    public static class PersonToPersonDtoMapping
    {
        public static PersonDto MapTo<TDestination>(this Person source)
            where TDestination : PersonDto
        {
            ArgumentNullException.ThrowIfNull(source);

            var destination = new PersonDto
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
