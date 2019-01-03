using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Twitter.Data.Contracts.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Twit> Twits { get; set; }
    }
}
