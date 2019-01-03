using Laba4.DataAccessLayer.EF;
using Laba4.DataAccessLayer.Entity;
using Laba4.DataAccessLayer.Identity;
using Laba4.DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TwifferDbContext dataBaseContext;

        private ApplicationUserManager userManager;

        private IRepository<Post> postRepository;

        public IRepository<Post> PostRepository
        {
            get
            {
                if (postRepository == null)
                    postRepository = new PostRepository(dataBaseContext);
                return postRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager;
            }
        }

        public EFUnitOfWork()
        {
            try
            {
                dataBaseContext = new TwifferDbContext();
                userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dataBaseContext));
            }
            catch(Exception ex)
            {
                string error = ex.Message;
            }
        }

        private bool disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    dataBaseContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            dataBaseContext.SaveChanges();
        }

    }
}
