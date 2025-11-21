using Domain.Entities;
using Domain.RepositoryInterfaces;
using Service.Abstraction;
using API.Dtos;
using System.Threading.Tasks;
using Shared.Helpers;
using Shared.Exceptions;

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
            Person person = new ()
            {
                
               Name = personDto.Name,
                
            };
            await repositoryManager.PersonRepository.AddAsync(person,cancellationToken);
            await repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            person = await repositoryManager.PersonRepository.GetByIdAsync(person.Id);
            return new PersonDto() {Id = person.Id,Name=person.Name };
        }

        public async Task DeletePerson(Guid id, CancellationToken cancellationToken = default)
        {
            await repositoryManager.PersonRepository.DeleteAsync(id);
        }

        public async Task<PersonDto> GetPersonById(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await repositoryManager.PersonRepository.GetByIdAsync(id, cancellationToken);

            if (person is null)
            {
                throw new PersonNotFoundException(id);
            }
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                
            };
        }

        public async Task<PagedList<PersonDto>> GetPersons(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var result = (await repositoryManager.PersonRepository.GetPagedAsync(pageNumber, pageSize, ct: cancellationToken));
                
             return result.Map((p)=> new PersonDto() { Id = p.Id, Name = p.Name });
            
        }

        public async Task<PersonDto> UpdatePerson(Guid id, PersonDto personDto, CancellationToken cancellationToken = default)
        {
            var person = await repositoryManager.PersonRepository.GetByIdAsync(id, cancellationToken) ?? throw new PersonNotFoundException(id);

            // Update the person entity with values from personDto
            person.Name = personDto.Name;

            await repositoryManager.PersonRepository.UpdateAsync(person);

            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
            };
        }
    }
}
