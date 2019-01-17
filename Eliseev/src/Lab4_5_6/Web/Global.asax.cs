using Autofac;
using Autofac.Integration.Mvc;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);            

            builder.RegisterModule<DIModule>();

            var conteiner = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(conteiner));
        }
    }
}
