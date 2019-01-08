using Autofac;
using Autofac.Core;
using Data.Repositories.Interfaces;
using Data.Repositories.Repositories;
using Domain.Contracts.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DIModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);


            builder.RegisterType<TwitterUnitOfWork>().As<IUnitOfWork>().AsSelf();

            builder.RegisterType<TwitService>().As<ITwitService>().WithParameter(new ResolvedParameter(
                (pi,ctx)=>pi.ParameterType==typeof(IUnitOfWork),
                (pi,ctx)=>ctx.Resolve<TwitterUnitOfWork>()));
            builder.RegisterType<UserService>().As<IUserService>().WithParameter(new ResolvedParameter(
                (pi,ctx)=>pi.ParameterType==typeof(IUnitOfWork),
                (pi,ctx)=>ctx.Resolve<TwitterUnitOfWork>()));
        }
    }
}
