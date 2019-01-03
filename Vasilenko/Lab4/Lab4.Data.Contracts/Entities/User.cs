using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Lab4.Data.Contracts.Entities
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public virtual ICollection<Post> Posts { get; set; }

        public User()
        {
            Posts = new List<Post>();
        }
    }
}
