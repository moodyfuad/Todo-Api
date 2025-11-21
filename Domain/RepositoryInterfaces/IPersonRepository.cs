using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddPersonAsync(Person person, CancellationToken cancellationToken = default);
        Task UpdateAsync(Entities.Person person);
        Task DeleteAsync(Guid id);
    }
}
