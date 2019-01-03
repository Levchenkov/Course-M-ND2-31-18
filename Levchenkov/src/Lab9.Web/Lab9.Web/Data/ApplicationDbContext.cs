using Lab9.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles
        {
            get;
            set;
        }

        public DbSet<Dialog> Dialogs
        {
            get;
            set;
        }

        public DbSet<Message> Messages
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var user = builder.Entity<ApplicationUser>();

            user.HasMany(x => x.Messages)
                .WithOne(x => x.Author);

            user.HasMany(x => x.Posts)
                .WithOne(x => x.Author);

            user.HasMany(x => x.Comments)
                .WithOne(x => x.Author);

            user.HasMany(x => x.Friends)
                .WithOne(x => x.SourceUser);

            user.HasOne(x => x.UserProfile)
                .WithOne(x => x.User)
                .HasForeignKey<ApplicationUser>(x => x.UserProfileId);

            var dialog = builder.Entity<Dialog>();

            dialog.HasMany(x => x.Messages)
                .WithOne(x => x.Dialog);

            builder.Entity<UserDialog>().HasKey(x => new { x.UserId, x.DialogId });

            builder.Entity<PostLike>().HasKey(x => new { x.PostId, x.UserId });
            builder.Entity<CommentLike>().HasKey(x => new { x.CommentId, x.UserId });

            var post = builder.Entity<Post>();

            post.HasMany(x => x.Comments)
                .WithOne(x => x.Post);

            var userProfile = builder.Entity<UserProfile>();

            userProfile.HasOne(x => x.User)
                .WithOne(x => x.UserProfile)
                .HasForeignKey<UserProfile>(x => x.UserId);

            var friend = builder.Entity<Friend>();

            friend.HasKey(x => new { x.SourceUserId, x.TargetUserId });
        }
    }
}
