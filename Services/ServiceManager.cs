using Domain.RepositoryInterfaces;
using Service.Abstraction;
using Services.JwtServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPersonServices> _lazyPersonServices;
        public IPersonServices PersonServices => _lazyPersonServices.Value;
        public Lazy<IAuth> _lazyAuth;

        public IAuth Auth => _lazyAuth.Value;

        public ServiceManager(IRepositoryManager repositoryManager, JwtHander jwtHander)
        {
            _lazyAuth = new Lazy<IAuth>(() => new Auth(repositoryManager, jwtHander));
            _lazyPersonServices = new Lazy<IPersonServices>(() => new PersonServices(repositoryManager));
        }
    }
}
