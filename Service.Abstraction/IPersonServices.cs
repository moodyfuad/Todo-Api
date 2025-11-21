using API.Dtos;
using Shared.Helpers;

namespace Service.Abstraction
{
    public interface IPersonServices
    {
        Task<PagedList<PersonDto>>GetPersons(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<PersonDto >GetPersonById(Guid id, CancellationToken cancellationToken = default);
        Task<PersonDto>CreatePerson(PersonDto personDto, CancellationToken cancellationToken = default);
        Task<PersonDto >UpdatePerson(Guid id, PersonDto personDto,CancellationToken cancellationToken = default);
        Task DeletePerson(Guid id,CancellationToken cancellationToken = default);

    }
}
