using Data.Repositories.Contexts;
using Data.Repositories.Entities;
using Data.Repositories.Identity;
using Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Repositories
{
    public class TwitterUnitOfWork : IUnitOfWork
    {
        private TwitterIdentityContext context;

        private TwitterUserManager userManager;

        private TwitterRoleManager roleManager;

        private IRepository<Twit> repository;

        public TwitterUnitOfWork()
        {
            context = new TwitterIdentityContext();
            userManager = new TwitterUserManager(new UserStore<User>(context));
            roleManager = new TwitterRoleManager(new RoleStore<Role>(context));
            repository = new TwitRepository(context);
        }

        public TwitterUserManager UserManager
        {
            get { return userManager; }
        }
        

        public TwitterRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<Twit> TwitRepository
        {
            get { return repository; }
        }
                
        public async Task SaveAsynk()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    context.Dispose();
                }
                disposed = true;
            }
        }


    }
}
