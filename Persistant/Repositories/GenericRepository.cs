using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistant.Repositories
{
    internal sealed class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T> where T : BaseEntity
    {
        public GenericRepository(RepositoryDbContext context) : base(context)
        {
        }
    }
}
