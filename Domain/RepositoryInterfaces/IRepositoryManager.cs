using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IRepositoryManager
    {
        IUnitOfWork UnitOfWork { get; }
        IPersonRepository PersonRepository { get;  }
        IGenericRepository<T> GetGenericRepository<T>() where T:BaseEntity;
        //IAppUserRepository AppUserRepository { get;  }
    }
}
