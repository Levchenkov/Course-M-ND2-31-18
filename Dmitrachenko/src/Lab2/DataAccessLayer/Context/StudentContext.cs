using System.Data.Entity;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Context
{
    public class StudentContext : DbContext
    {
        static StudentContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentContext>());
        }

        public StudentContext() : base("StudentContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var student = modelBuilder.Entity<Student>();
            student.HasKey(s => s.Id);
            student.HasMany(s => s.Posts)
                .WithRequired(p => p.Author)
                .HasForeignKey(p => p.AuthorId).WillCascadeOnDelete(false);
            student.HasMany(s => s.Comments)
                .WithRequired(c => c.Author)
                .HasForeignKey(c => c.AuthorId).WillCascadeOnDelete(false);

            var post = modelBuilder.Entity<Post>();
            post.HasKey(p => p.Id);
            post.Property(p => p.Created).IsRequired().HasColumnType("datetime2");
            post.HasMany(p => p.Comments).WithRequired(c => c.Post).HasForeignKey(c => c.PostId);
            post.HasMany(p => p.Tags).WithMany(t => t.Posts).Map(c =>
            {
                c.MapLeftKey("PostId");
                c.MapRightKey("TagId");
                c.ToTable("PostTag");
            });

            var tag = modelBuilder.Entity<Tag>();
            tag.HasKey(t => t.Id);

            var comment = modelBuilder.Entity<Comment>();
            comment.HasKey(c => c.Id);
            comment.Property(c => c.Created).IsRequired().HasColumnType("datetime2");
        }

        public virtual IDbSet<Post> Posts { get; set; }
        public virtual IDbSet<Tag> Tags { get; set; }
        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
    }
}
