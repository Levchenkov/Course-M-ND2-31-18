using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Data.Repositories.Entities;

namespace Data.Repositories.Contexts
{
    public class TwitterIdentityContext:IdentityDbContext<User>
    {
        public DbSet<Twit> Twits { get; set; }

        public TwitterIdentityContext() : base("name=Lab456")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Twit>()
                .HasRequired(t => t.User)
                .WithMany(a => a.Twits)
                .HasForeignKey<string>(t => t.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
