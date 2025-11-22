using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Abstraction;
using Services.JwtServices;
using Shared.Helpers;
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
        private readonly Lazy<IPersonGroupServices> _lazyPersonGroupServices;
        private readonly Lazy<ITaskServices> _lazyTaskServices;
        private readonly Lazy<INoteServices> _lazyNoteServices;
        private readonly Lazy<IJwtService> _lazyjwtService;
        public IPersonServices PersonServices => _lazyPersonServices.Value;
        public IPersonGroupServices PersonGroupServices => _lazyPersonGroupServices.Value;
        public ITaskServices TaskServices => _lazyTaskServices.Value;
        public INoteServices NoteServices => _lazyNoteServices.Value;

        public Lazy<IAuth> _lazyAuth;

        public IAuth Auth => _lazyAuth.Value;

        public IJwtService JwtService => _lazyjwtService.Value;

        //public ServiceManager(IRepositoryManager repositoryManager, JwtHander jwtHander)
        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
        {
            _lazyPersonServices = new Lazy<IPersonServices>(() => new PersonServices(repositoryManager));
            _lazyPersonGroupServices = new Lazy<IPersonGroupServices>(() => new PersonGroupServices(repositoryManager));
            _lazyTaskServices  = new Lazy<ITaskServices>(() => new TaskServices(repositoryManager));
            _lazyNoteServices = new Lazy<INoteServices>(() => new NoteServices(repositoryManager));
            _lazyjwtService = new Lazy<IJwtService>(() => new JwtService(configuration));
            _lazyAuth = new Lazy<IAuth>(() => new Auth(repositoryManager, JwtService));
        }
    }
}
