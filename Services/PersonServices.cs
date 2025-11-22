using Domain.Entities;
using Domain.RepositoryInterfaces;
using Service.Abstraction;
using Services.Mappings.PersonMappings;
using Shared.Dtos;
using Shared.Dtos.Pagination;
using Shared.Exceptions;
using Shared.Helpers;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class PersonServices : IPersonServices
    {
        private readonly IRepositoryManager repositoryManager;

        public PersonServices(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        public async Task<PersonDto> CreatePerson(PersonDto personDto, CancellationToken cancellationToken = default)
        {
            bool exist = await repositoryManager.PersonRepository.Exist(p => p.Username == personDto.Username, ct: cancellationToken);
            if (exist)
            {
                throw new PersonAlreadyExistsException(personDto.Username);
            }
          
            Person person = personDto.MapTo<Person>();
            person.Id = Guid.NewGuid();
            await repositoryManager.PersonRepository.AddAsync(person,cancellationToken);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return person.MapTo<PersonDto>();
        }

        public async Task DeletePerson(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await repositoryManager.PersonRepository.GetByIdAsync(id,ct: cancellationToken);            
            repositoryManager.PersonRepository.Remove(person);
        }

        public async Task<PersonDto> GetPersonById(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await repositoryManager.PersonRepository.GetByIdAsync(id,ct: cancellationToken);
            return person.MapTo<PersonDto>();
        }

        public async Task<PagedList<PersonDto>> GetPersons(PersonSearchParameters searchParameters, CancellationToken cancellationToken = default)
        {

            var result = (await repositoryManager.PersonRepository.
                GetPagedAsync(
                searchParameters.PageNumber,
                searchParameters.PageSize,
                predicate: p => p.Username.ToLower().StartsWith(searchParameters.Username??"".ToLower()),
                ct: cancellationToken));

            return result.Map((p) => p.MapTo<PersonDto>());
        }

        public async Task<PersonDto> UpdatePerson(Guid id, PersonDto personDto, CancellationToken cancellationToken = default)
        {
            var person = await repositoryManager.PersonRepository.GetByIdAsync(id, ct: cancellationToken);

            repositoryManager.PersonRepository.Update(personDto.MapTo<Person>());

            return personDto;
        }
    }
}
