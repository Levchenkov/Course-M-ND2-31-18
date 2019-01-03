using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laba3;

namespace Lab3mvc
{
    public class SimpleIocControllerFactory : DefaultControllerFactory
    {
        private readonly IMyContainer container;


        public SimpleIocControllerFactory(IMyContainer container)
        {
            this.container = container;
        }


        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return container.Resolve(controllerType) as Controller;
        }
    }
}