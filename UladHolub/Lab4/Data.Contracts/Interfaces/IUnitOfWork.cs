using Data.Contracts.Entities;
using System;
using System.Threading.Tasks;

namespace Data.Contracts.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserManager UserManager { get; }
        IAppRoleManager RoleManager { get; }
        IRepository<Post> PostRepository { get; }
        Task SaveAsync();
    }
}
