using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IStudentService>().To<StudentService>();
            Bind<IPostService>().To<PostService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<ITagService>().To<TagService>();
        }
    }
}