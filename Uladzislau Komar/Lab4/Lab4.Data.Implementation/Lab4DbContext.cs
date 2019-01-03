using System;
using System.Collections.Generic;
using System.Text;
using Lab4.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Implementation
{
    public class Lab4DbContext : IdentityDbContext<PersonEntity, IdentityRole<int>, int>
    {
        public Lab4DbContext(DbContextOptions<Lab4DbContext> options)
            : base(options)
        { }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<TweetEntity> LabTweets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>()
                .HasMany(t => t.TweetList)
                .WithOne(t => t.Author)
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
