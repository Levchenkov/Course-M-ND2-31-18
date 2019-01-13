using Autofac;
using Autofac.Integration.Mvc;
using Domain.Contracts.Interfaces;
using Domain.Services;
using Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly:OwinStartup(typeof(Web.App_Start.Startup))]

namespace Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // var hubConfig = new HubConfiguration() { EnableDetailedErrors=true,EnableJSONP=true,EnableJavaScriptProxies=true};
            //app.MapSignalR(hubConfig);
            app.MapSignalR(); ;
            



            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });            
        }

    }
}