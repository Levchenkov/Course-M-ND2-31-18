using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab4.MyService.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("IdentityDb") { }
        public DbSet<Post> Posts { get; set; }



    }
}