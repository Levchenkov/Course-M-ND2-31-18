using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Ninject.Modules;
using Lab2.Util;
using Ninject;
using Ninject.Web.Mvc;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Infrastructure;
using DataAccessLayer.Interfaces;

namespace Lab2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            NinjectModule registrations = new NinjectRegistrations();
            NinjectModule serviceModule = new ServiceModule();
            NinjectModule autoMapper = new AutoMapperModule();
            var kernel = new StandardKernel(registrations, serviceModule, autoMapper);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));


        }
    }
}
