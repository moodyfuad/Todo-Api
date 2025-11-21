using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistant.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistant.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        //Person Repository

        private readonly Lazy<IPersonRepository> _lazyPersonRepository;
        public IPersonRepository PersonRepository => _lazyPersonRepository.Value;

        // UsersRepository
        private readonly Lazy<IAppUserRepository> _lazyAppUserRepository;
        public IAppUserRepository AppUserRepository => _lazyAppUserRepository.Value;

        //UnitOfWork
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;


        public RepositoryManager(RepositoryDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._userManager = userManager;

            _lazyAppUserRepository ??= new Lazy<IAppUserRepository>(() => new AppUserRepository(_userManager));
            _lazyPersonRepository ??= new Lazy<IPersonRepository>(() => new PersonRepository(_dbContext));
            _lazyUnitOfWork ??= new Lazy<IUnitOfWork>(() => new UnitOfWork(_dbContext));
        }


    
    }
}
