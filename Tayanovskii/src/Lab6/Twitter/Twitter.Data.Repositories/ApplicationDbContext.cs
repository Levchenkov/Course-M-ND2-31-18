using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Twitter.Data.Contracts.Entities;

namespace Twitter.Data.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasMany(user => user.Posts).WithOne(post => post.Author).OnDelete(DeleteBehavior.SetNull);

            var postEntity = builder.Entity<Post>();
            postEntity.HasKey(post => post.Id);
            postEntity.Property(post => post.Content).HasMaxLength(240).IsRequired().IsUnicode();
            postEntity.Property(post => post.CreatedTime).IsRequired();
        }

        public virtual DbSet<Post> Posts { get; set; }
    }
}
