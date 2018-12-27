using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DI
{
    public class DIFactory: DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return dIContainer.Create(controllerType) as Controller;
        }

        private DIContainer dIContainer;

        public DIFactory(DIContainer container)
        {
            dIContainer = container;
        }
    }
}
