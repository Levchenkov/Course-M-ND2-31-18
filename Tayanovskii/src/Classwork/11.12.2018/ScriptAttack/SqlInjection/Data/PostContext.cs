using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SqlInjection.Models
{
    public class PostContext : DbContext
    {
        public PostContext (DbContextOptions<PostContext> options)
            : base(options)
        {
        }

        public DbSet<SqlInjection.Models.Post> Post { get; set; }
    }
}
