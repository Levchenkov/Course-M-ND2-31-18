using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly StudentContext dbContext;

        public PostRepository(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Post> GetAll()
        {
            return dbContext.Posts;
        }
        public Post Get(int id)
        {
            return dbContext.Posts.Find(id);
        }
        public void Create(Post item)
        {
            dbContext.Posts.Add(item);
        }
        public void Update(Post item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var deletePost = dbContext.Posts.Find(id);
            if (deletePost != null) dbContext.Posts.Remove(deletePost);
        }
        public IEnumerable<Post> Find(Func<Post, bool> predicate)
        {
            return dbContext.Posts.Where(predicate);
        }
    }
}
