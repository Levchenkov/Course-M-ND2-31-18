using Ninject.Modules;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1.Util
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
            Bind<IStudentRepository>().To<StudentRepository>();
            //Bind<IOrder>().To<CacheOrder>();
        }
    }
}