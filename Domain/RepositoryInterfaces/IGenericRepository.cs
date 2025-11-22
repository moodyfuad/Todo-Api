using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.RepositoryInterfaces
{
    public interface IGenericRepository<T>: IBaseRepository<T> where T : BaseEntity
    {
    }
}
