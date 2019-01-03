using Data.Contracts.Entities;
using Data.Contracts.Interfaces;
using Data.Repositories.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repositories.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private AppIdentityDbContext dataBase;

        public PostRepository(AppIdentityDbContext context)
        {
            dataBase = context;
        }

        public IEnumerable<Post> GetAll()
        {
            return dataBase.Posts;
        }

        public IEnumerable<Post> GetLastestRecords(int recordsNumber)
        {
            return dataBase.Posts.OrderByDescending(p => p.Date).Take(recordsNumber);
        }

        public Post Get(string id)
        {
            return dataBase.Posts.Find(id);
        }

        public void Create(Post post)
        {
            dataBase.Posts.Add(post);
        }

        public void Update(Post post)
        {
            dataBase.Entry(post).State = EntityState.Modified;
        }

        public IEnumerable<Post> Find(Func<Post, Boolean> predicate)
        {
            return dataBase.Posts.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Post post = dataBase.Posts.Find(id);

            if (post != null) { dataBase.Posts.Remove(post); }
        }
    }
}
