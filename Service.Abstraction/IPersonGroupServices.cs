using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.PersonDtos;
using Shared.Dtos.TaskItemGroupDtos;
using Shared.Helpers;
using Shared.OperationResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstraction
{
    public interface IPersonGroupServices
    {
        Task<PagedList<PersonGroupDto>> GetUserGroups(Guid personId, PaginationParameters parameters);
        Task<PersonGroupDto> GetUserGroup(Guid groupId);
        Task<OperationResult> RemoveMemberFromGroup(Guid acationBy, Guid groupId, Guid memberId);
        Task<OperationResult> CreateGroup(Guid creatorId, CreateTaskItemGroupDto dto);
        Task<OperationResult> AddMemberToGroup(Guid acationBy, Guid groupId, AddMemberToGroupDto dto);
        Task<IEnumerable<GetGroupUserDto>> GetGroupUsers(Guid groupId);
        Task<OperationResult> ChangeMemberRole(Guid groupId, Guid memberId, GroupMemnerRolesDto role);
    }
}
