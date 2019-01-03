using Lab4.Data.Contracts.Entities;
using Lab4.Data.Contracts.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab4.Data.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> User { get; set; }
        public IQueryable<Post> PostsRepository => this.Posts;
        public IQueryable<User> UsersRepository => this.User;

       

        public void Save()
        {
            SaveChanges();
        }

        public void Add<TEntity>(IQueryable<TEntity> repository, TEntity entity)
            where TEntity : class
        {
            (repository as DbSet<TEntity>).Add(entity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


     /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;");
            }
        }*/

    }
}
