using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Twitter.Data.Contracts.Entities
{
    public class User: IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
    }
}
