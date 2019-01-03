using Laba4.DataAccessLayer.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.EF
{
    public class TwifferDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }

        public TwifferDbContext() : base("Laba4DbContext")
        {
        }

    }
}
