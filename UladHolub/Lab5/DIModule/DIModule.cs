using Autofac;
using Autofac.Core;
using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Data.Repositories.EF;
using Data.Repositories.Repositories;
using Domain.Contracts.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DIModule
{
    public class DIModule : Module
    {
        public string ConnectionString { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var x = new AppIdentityDbContext();
            //builder.Register<AppIdentityDbContext>(c => x);
            //builder.Register<UserStore<User>>(c => new UserStore<User>(x)).AsImplementedInterfaces();
            //builder.Register<RoleStore<Role>>(c => new RoleStore<Role>(x)).AsImplementedInterfaces();
            //builder.Register<IdentityFactoryOptions<AppUserManager>>(c => new IdentityFactoryOptions<AppUserManager>()
            //{
            //    DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Data.Repositories")
            //});
            //builder.Register<IdentityFactoryOptions<AppRoleManager>>(c => new IdentityFactoryOptions<AppRoleManager>()
            //{
            //    DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Data.Repositories")
            //});
            //builder.RegisterType<AppUserManager>().As<IAppUserManager>().AsSelf();
            //builder.RegisterType<AppRoleManager>().As<IAppRoleManager>().AsSelf();
            builder.RegisterType<AppUnitOfWork>().As<IUnitOfWork>().AsSelf();
            builder.RegisterType<Domain.Services.Services.DomainUnitOfWork>().As<IDomainUnitOfWork>().AsSelf();

            builder.RegisterType<Domain.Services.Services.DomainUnitOfWork>().As<IDomainUnitOfWork>().WithParameter(
                new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IUnitOfWork),
                    (pi, ctx) => ctx.Resolve<AppUnitOfWork>()));
        }
    }
}
