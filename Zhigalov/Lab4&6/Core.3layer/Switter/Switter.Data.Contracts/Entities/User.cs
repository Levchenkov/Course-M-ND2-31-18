using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Switter.Data.Contracts.Entities
{
    public class User : IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        public IEnumerable<Tweet> Tweets { get; set; }
    }
}
