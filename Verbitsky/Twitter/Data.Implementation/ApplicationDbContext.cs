using Data.Contracts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementation
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public virtual DbSet<TweetEntity> Tweets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasMany<TweetEntity>(a => a.Tweets)
                .WithOne(a => a.Author);
        }
    }
}
