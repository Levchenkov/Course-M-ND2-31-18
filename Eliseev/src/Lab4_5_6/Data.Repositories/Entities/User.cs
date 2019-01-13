using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Data.Repositories.Entities
{
    public class User:IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public ICollection<Twit> Twits
        {
            get;
            set;
        }
    }
}
