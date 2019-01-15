using ItNews.Data.Contracts.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItNews.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<Profile> Profiles
        {
            get;
            set;
        }

        public DbSet<Post> Posts
        {
            get;
            set;
        }

        public DbSet<Comment> Comments
        {
            get;
            set;
        }

        public DbSet<Category> Categories
        {
            get;
            set;
        }

        public DbSet<Like> Likes
        {
            get;
            set;
        }

        public DbSet<PostTag> PostTags
        {
            get;
            set;
        }

        public DbSet<Rating> Ratings
        {
            get;
            set;
        }

        public DbSet<Tag> Tags
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });

            builder.Entity<Category>().Property(x => x.Value).IsRequired();

            builder.Entity<Tag>().Property(x => x.Value).IsRequired();

            builder.Entity<Comment>().Property(x => x.Content).IsRequired();
            
            var profile = builder.Entity<Profile>();

            profile.Property(x => x.UserId).IsRequired();
            profile.Property(x => x.FirstName).IsRequired();
            profile.Property(x => x.LastName).IsRequired();
            profile.Property(x => x.Nickname).IsRequired();

            var post = builder.Entity<Post>();

            post.Property(x => x.Title).IsRequired();
            post.Property(x => x.Content).IsRequired();
        }
    }
}
