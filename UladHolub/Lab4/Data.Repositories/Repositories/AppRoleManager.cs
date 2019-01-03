using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Data.Repositories.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Data.Repositories.Repositories
{
    public class AppRoleManager : RoleManager<Role>, IAppRoleManager
    {
        public AppRoleManager(RoleStore<Role> store)
            : base(store)
        { }
        
        //public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options,
        //    IOwinContext context)
        //{
        //    return new AppRoleManager(new RoleStore<Role>(context.Get<AppIdentityDbContext>()));
        //}
    }
}
