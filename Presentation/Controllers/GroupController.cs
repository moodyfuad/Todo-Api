using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Responses;
using Service.Abstraction;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.PersonDtos;
using Shared.Dtos.TaskItemGroupDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GroupController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{groupId}/members")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetGroupUserDto>>>> GetGroupMembers(Guid groupId)
        {
            var result = await _serviceManager.PersonGroupServices.GetGroupUsers(groupId);

            return ApiResponse< IEnumerable < GetGroupUserDto >>.Success(result);
        }

        [HttpPost("{groupId}/member")]
        public async Task<ActionResult<ApiResponse<bool>>> AddGroupMember(Guid groupId, [FromBody] AddMemberToGroupDto dto)
        {
            var userId = User.GetUserId();
            var result = await _serviceManager.PersonGroupServices.AddMemberToGroup( userId,groupId, dto);
            if (result.IsSuccess)
            {
                return Created();  
            }
            return BadRequest(ApiResponse<bool>.Fail("Failed to Add member",errors: result.ErrorMessage));
        }

        [HttpDelete("{groupId}/member/{memberId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteGroupMember(Guid groupId, Guid memberId)
        {
            var userId = User.GetUserId();
            var result = await _serviceManager.PersonGroupServices.RemoveMemberFromGroup(userId, groupId, memberId);
            if (result.IsSuccess)
            {
                return Created();  
            }
            return BadRequest(ApiResponse<bool>.Fail("Failed to Add member",errors: result.ErrorMessage));
        }
        [HttpPut("{groupId}/member/{memberId}/role")]
        public async Task<ActionResult<ApiResponse<bool>>> ChangeMemberRole(Guid groupId, Guid memberId, GroupMemnerRolesDto role)
        {
            var result = await _serviceManager.PersonGroupServices.ChangeMemberRole(groupId, memberId, role);
            if (result.IsSuccess)
            {
                return Created();  
            }
            return BadRequest(ApiResponse<bool>.Fail("Failed to Add member",errors: result.ErrorMessage));
        }
    }
}
