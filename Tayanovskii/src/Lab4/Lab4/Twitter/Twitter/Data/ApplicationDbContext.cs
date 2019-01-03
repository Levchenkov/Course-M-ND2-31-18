using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Twitter.Data.Entities;
using Twitter.Models;

namespace Twitter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasMany(user => user.Posts).WithOne(post => post.Author).OnDelete(DeleteBehavior.SetNull);

            var postEntity = builder.Entity<Post>();
            postEntity.HasKey(post => post.Id);
            postEntity.Property(post => post.Content).HasMaxLength(240).IsRequired().IsUnicode();
            postEntity.Property(post => post.CreatedTime).IsRequired();
        }

        public virtual DbSet<Post> Posts { get; set; }
    }
}
