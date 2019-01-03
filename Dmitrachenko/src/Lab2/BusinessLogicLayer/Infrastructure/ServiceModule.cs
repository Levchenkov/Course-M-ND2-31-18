using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Ninject.Modules;

namespace BusinessLogicLayer.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>();
        }
    }
}
