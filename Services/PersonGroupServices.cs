using Domain.Entities;
using Domain.Enums;
using Domain.RepositoryInterfaces;
using Service.Abstraction;
using Services.Mappings.GroupMappings;
using Services.Mappings.UserRolesMappers;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Dtos.PersonDtos;
using Shared.Dtos.TaskItemGroupDtos;
using Shared.Exceptions;
using Shared.Helpers;
using Shared.OperationResults;

namespace Services
{
    public class PersonGroupServices : IPersonGroupServices
    {
        private readonly IRepositoryManager repos;

        public PersonGroupServices(IRepositoryManager repos)
        {
            this.repos = repos;
        }
        public async Task<PagedList<PersonGroupDto>> GetUserGroups(Guid personId, PaginationParameters parameters)
        {
            var groups = await repos.GetGenericRepository<PersonTaskItemGroup>().
                GetPagedAsync(
                parameters.PageNumber,
                parameters.PageSize,
                g => g.PersonId == personId,
                includes: [g => g.TaskItemGroup, g => g.TaskItemGroup.CreatedBy]);
           
            var personGroupDtos = groups.Map<PersonGroupDto>(group => new PersonGroupDto
            {
                Id = group.TaskItemGroupId,
                Name = group.TaskItemGroup.Name,
                Description = group.TaskItemGroup.Description ?? "",
                CreatorName = group.TaskItemGroup.CreatedBy.FirstName + " " + group.TaskItemGroup.CreatedBy.LastName

            });
            return personGroupDtos;
        }

        public async Task<OperationResult> AddMemberToGroup(Guid acationBy, Guid groupId, AddMemberToGroupDto dto)
        {
            var group = await repos.GetGenericRepository<TaskItemGroup>().
                FindAsync(
                g => g.Id == groupId, 
                includes: [g => g.PersonGroups]);

            var personGroup = await repos.GetGenericRepository<PersonTaskItemGroup>().
                FindAsync(
                pg => pg.PersonId == acationBy && pg.TaskItemGroupId == groupId
                );
            if (personGroup.PersonRole != UserRoles.Admin)
            {
                throw new AccessForbiddenException("Only Admins can add members to the group");
            }
           
            var person = await repos.GetGenericRepository<Person>().FindAsync(p => p.Id == dto.MemberId);
           bool alreadyAdded = await repos.GetGenericRepository<PersonTaskItemGroup>().
                Exist(
                pig => pig.PersonId == dto.MemberId && pig.TaskItemGroupId == groupId
                );

            if (alreadyAdded)
            {
                return OperationResult.Success();
            }
            PersonTaskItemGroup taskItemGroup = new ()
            {
                PersonId = dto.MemberId,
                TaskItemGroupId = groupId,
                PersonRole = dto.MemberRole.MapToUserRole(),
                
            };
            await repos.GetGenericRepository<PersonTaskItemGroup>().
                AddAsync(taskItemGroup);
            
            await repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();

        }

        public async Task<OperationResult> CreateGroup(Guid creatorId, CreateTaskItemGroupDto dto)
        {
            var creator = await repos.GetGenericRepository<Person>().
                GetByIdAsync(creatorId);

            if (creator == null) return OperationResult.Failure("Creator not found");

            TaskItemGroup group = new TaskItemGroup
            {
                Name = dto.Title,
                Description = dto.Description,
                CreatedById = creatorId
            };
            group.PersonGroups.Add(new PersonTaskItemGroup
            {
                PersonId = creatorId,
                TaskItemGroupId = group.Id,
                PersonRole = UserRoles.Admin
            });
            await repos.GetGenericRepository<TaskItemGroup>().AddAsync(group);
            await repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<PersonGroupDto> GetUserGroup(Guid groupId)
        {
            var group = await repos.GetGenericRepository<TaskItemGroup>().
                GetByIdAsync(groupId);
            return group.MapTo<PersonGroupDto>();
        }

        public async Task<OperationResult> RemoveMemberFromGroup(Guid acationById, Guid groupId, Guid memberId)
        {
            var group = await repos.GetGenericRepository<TaskItemGroup>().FindAsync(g => g.Id == groupId);

            var exist = await repos.GetGenericRepository<PersonTaskItemGroup>().
                Exist(
                g => g.TaskItemGroupId == groupId && g.PersonId == memberId,
                includes: [pg=> pg.TaskItemGroup, pg => pg.Person]);

            if (!exist)
            {
                return OperationResult.Success();
            }
            var persongroup = await repos.GetGenericRepository<PersonTaskItemGroup>().
                FindAsync(
                g => g.TaskItemGroupId == groupId && g.PersonId == memberId,
                includes: [pg=> pg.TaskItemGroup, pg => pg.Person]);
            if (persongroup.TaskItemGroup.CreatedById == memberId)
            {
                return OperationResult.Failure("Can not delete Group Admin");
            }
            persongroup = await repos.GetGenericRepository<PersonTaskItemGroup>().
                FindAsync(
                g => g.TaskItemGroupId == groupId && g.PersonId == acationById,
                includes: [pg => pg.TaskItemGroup, pg => pg.Person]);
            if (persongroup.PersonRole != UserRoles.Admin)
            {
                throw new AccessForbiddenException("Only Admins have access to remove members");
            }

            repos.GetGenericRepository<PersonTaskItemGroup>().Remove(persongroup);
            await repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }

        public async Task<IEnumerable<GetGroupUserDto>> GetGroupUsers(Guid groupId)
        {
            var personGroups = await repos.GetGenericRepository<PersonTaskItemGroup>().
                 FindRangeAsync(
                predicate: pg => pg.TaskItemGroupId == groupId,
                 includes: [pg => pg.Person, pg => pg.TaskItemGroup]
                 );
            if (personGroups is null || !personGroups.Any())
            {
                return [];
            }

            return personGroups.Select(
                pg => new GetGroupUserDto()
                {
                    Id = pg.PersonId,
                    Name = pg.Person.FirstName + " " + pg.Person.LastName,
                    Username = pg.Person.Username,
                    RoleName = pg.PersonRole.ToString()
                    
                });
        }

        public async Task<OperationResult> ChangeMemberRole(Guid groupId, Guid memberId, GroupMemnerRolesDto role){
            var personGroup = await repos.GetGenericRepository<PersonTaskItemGroup>().
                FindAsync(
                pig => pig.PersonId == memberId && pig.TaskItemGroupId == groupId,
                trackChanges: true);

            personGroup.PersonRole = role.MapToUserRole();
            await repos.UnitOfWork.SaveChangesAsync();
            return OperationResult.Success();
        }
    }
}
