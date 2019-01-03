using Data.Contracts.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Data.Repositories.EF
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }

        public AppIdentityDbContext() : base("name=Lab4Db") { }

        static AppIdentityDbContext()
        {
            System.Data.Entity.Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.Posts);

            base.OnModelCreating(modelBuilder);
        }
    }
}
