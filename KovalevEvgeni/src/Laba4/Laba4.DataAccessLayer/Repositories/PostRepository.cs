using Laba4.DataAccessLayer.EF;
using Laba4.DataAccessLayer.Entity;
using Laba4.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private TwifferDbContext dataBaseContext;

        public PostRepository(TwifferDbContext dataBase)
        {
            dataBaseContext = dataBase;
        }

        public void Create(Post item)
        {
            dataBaseContext.Posts.Add(item);
        }

        public void Delete(int recordId)
        {
            Post record = GetRecord(recordId);
            if (record != null)
                dataBaseContext.Posts.Remove(record);
        }

        public IQueryable<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            return dataBaseContext.Posts.Include("User").Where(predicate);
        }

        public IQueryable<Post> GetAll()
        {
            return dataBaseContext.Posts.Include("User");
        }

        public Post GetRecord(int recordId)
        {
            return dataBaseContext.Posts.Include("User").FirstOrDefault(s => s.PostId == recordId);
        }

        public void Update(Post item)
        {
            dataBaseContext.Entry(item).State = EntityState.Modified;
        }
    }
}
