using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.TaskItemGroupDtos
{
    public class GetGroupUserDto
    {
        public Guid Id { get; set; }    
        public required string Name { get; set; }

        public required string Username { get; set; }

        public required string RoleName { get; set; }
    }
}
