using Data.Contracts.Entities;
using Data.Repositories.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Data.Repositories.EF
{
    public class IdentityDbInit : CreateDatabaseIfNotExists<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            var userManager = new AppUserManager(new UserStore<User>(context));
            var roleManager = new AppRoleManager(new RoleStore<Role>(context));
            var role1 = new Role { Name = "admin" };
            var role2 = new Role { Name = "user" };
            roleManager.Create(role1);
            roleManager.Create(role2);
            var admin = new User { Email = "uladzislau.holub@gmail.com", UserName = "Admin"};
            string password = "Qazwsx1";
            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
        }
    }
}
