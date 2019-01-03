using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Twitter.Data.Contracts.Entities;


namespace Twitter.Data.Contracts.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Twit> Twits { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TwitTag> TwitTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TwitTag>()
                .HasKey(tt => new { tt.TwitId, tt.TagId });

            builder.Entity<TwitTag>()
                .HasOne(t => t.Twit)
                .WithMany(t => t.TwitTags)
                .HasForeignKey(t => t.TwitId);

            builder.Entity<TwitTag>()
                .HasOne(t => t.Tag)
                .WithMany(t => t.TwitTags)
                .HasForeignKey(t => t.TagId);
        }
    }
}
