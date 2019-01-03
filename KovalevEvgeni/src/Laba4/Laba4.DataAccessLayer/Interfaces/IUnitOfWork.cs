using Laba4.DataAccessLayer.Entity;
using Laba4.DataAccessLayer.Identity;
using Laba4.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IRepository<Post> PostRepository { get; }
        void Save();
    }
}
