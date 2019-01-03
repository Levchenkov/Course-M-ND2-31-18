using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Lab3mvc.Controllers;
using Lab3mvc.Repository;
using Lab3mvc.Service;
using Laba3;

namespace Lab3mvc
{
    public class ConfigFile
    {
        public static void Configure(IMyContainer container)
        {
            container.Register<HomeController, HomeController>();
            container.Register<IStudentRepository<string>, StudentRepository>();
            container.Register<IStudentService, StudentService>();
        }
    }
}