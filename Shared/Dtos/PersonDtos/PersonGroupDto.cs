using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.PersonDtos
{
    public class PersonGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string CreatorName { get; set; }


    }
}
