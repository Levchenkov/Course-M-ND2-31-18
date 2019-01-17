using Data.Repositories.Entities;
using Data.Repositories.Identity;
using System;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        TwitterUserManager UserManager
        {
            get;
        }
        TwitterRoleManager RoleManager
        {
            get;
        }
        IRepository<Twit> TwitRepository
        {
            get;
        }
        Task SaveAsynk();
    }
}
