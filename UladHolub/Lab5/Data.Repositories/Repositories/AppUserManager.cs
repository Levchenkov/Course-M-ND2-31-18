using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Data.Repositories.EF;
using Data.Repositories.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Data.Repositories.Repositories
{
    public class AppUserManager : UserManager<User>, IAppUserManager
    {
        public AppUserManager(IUserStore<User> store)
            : base(store)
        {
            EmailService = new EmailService();

            var provider = new DpapiDataProtectionProvider("Lab4");
            UserTokenProvider = new DataProtectorTokenProvider<User, string>(provider.Create("Lab4Token"))
                as IUserTokenProvider<User, string>;
        }

        //public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
        //    IOwinContext context)
        //{
        //    AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
        //    AppUserManager manager = new AppUserManager(new UserStore<User>(db));
        //    return manager;
        //}
    }
}
