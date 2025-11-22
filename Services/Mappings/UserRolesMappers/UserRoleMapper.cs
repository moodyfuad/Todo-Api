using Domain.Enums;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services.Mappings.UserRolesMappers
{
    public static class UserRoleMapper
    {
        public static UserRoles MapToUserRole(this GroupMemnerRolesDto dto)
        {
            switch (dto)
            {
                case GroupMemnerRolesDto.Admin:
                    return UserRoles.Admin;
                case GroupMemnerRolesDto.Editor:
                    return UserRoles.Editor;
                    
                case GroupMemnerRolesDto.Viewer:
                    return UserRoles.Viewer;

                default: return UserRoles.Viewer;
            }
        }
    }
}