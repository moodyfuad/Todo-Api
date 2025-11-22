using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.TaskItemGroupDtos
{
    public class AddMemberToGroupDto
    {
        public Guid MemberId { get; set; }
        public GroupMemnerRolesDto MemberRole { get; set; } = GroupMemnerRolesDto.Viewer;
    }
}
