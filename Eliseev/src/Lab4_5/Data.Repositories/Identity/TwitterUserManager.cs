using Data.Repositories.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

namespace Data.Repositories.Identity
{
    public class TwitterUserManager:UserManager<User>
    {
        public TwitterUserManager(IUserStore<User> store) : base(store)
        {
            EmailService = new EmailService();

            var provider = new DpapiDataProtectionProvider("Lab_4_5_6");
            UserTokenProvider = new DataProtectorTokenProvider<User, string>(provider.Create("Lab456Token")); 
        }

    }
}
