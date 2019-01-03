using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab_2.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
           : base("name=DataBase")
        {
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasMany<Post>(a => a.Posts).WithRequired(a => a.Author).HasForeignKey(a => a.AuthorId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>().HasMany<Comment>(a => a.Comments).WithRequired(a => a.Author).HasForeignKey(a => a.AuthorId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>().HasMany<Comment>(a => a.Comments).WithRequired(a => a.Post).HasForeignKey(a => a.PostId);

            modelBuilder.Entity<Post>().HasMany<Tag>(a => a.Tags).WithMany(a => a.Posts).Map(a =>
            {
                a.MapLeftKey("TagsId");
                a.MapRightKey("PostsId");
                a.ToTable("TagsPosts");
            });
        }
    }
}