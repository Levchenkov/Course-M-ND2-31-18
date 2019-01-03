using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Data.Contracts.Entities
{
    public class Role : IdentityRole
    {
        public override ICollection<IdentityUserRole> Users => base.Users;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
