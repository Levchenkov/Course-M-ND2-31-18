using Data.Repositories.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Data.Repositories.Identity
{
    public class TwitterRoleManager:RoleManager<Role>
    {
        public TwitterRoleManager(RoleStore<Role> store) : base(store) { }
    }
}
