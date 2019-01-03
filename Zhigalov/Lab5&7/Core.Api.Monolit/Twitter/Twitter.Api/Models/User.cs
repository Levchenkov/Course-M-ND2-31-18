using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Api.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Tweet> Tweets { get; set; }
    }
}
