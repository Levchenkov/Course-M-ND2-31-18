using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Data.Repositories.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.Repositories
{
    public class AppUnitOfWork : IUnitOfWork
    {
        private AppIdentityDbContext database;
        private IAppRoleManager roleManager;
        private IAppUserManager userManager;
        private IRepository<Post> postRepository;

        public AppUnitOfWork()
        {
            database = new AppIdentityDbContext();
            roleManager = new AppRoleManager(new RoleStore<Role>(database));
            userManager = new AppUserManager(new UserStore<User>(database));
            postRepository = new PostRepository(database);
        }

        public IAppRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IAppUserManager UserManager
        {
            get { return userManager; }
        }

        public IRepository<Post> PostRepository
        {
            get { return postRepository; }
        }

        public async Task SaveAsync()
        {
            await database.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    database.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
