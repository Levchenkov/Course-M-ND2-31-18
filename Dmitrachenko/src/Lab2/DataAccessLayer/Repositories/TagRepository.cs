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
    public class TagRepository : IRepository<Tag>
    {
        private readonly StudentContext dbContext;

        public TagRepository(StudentContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Tag> GetAll()
        {
            return dbContext.Tags;
        }
        public Tag Get(int id)
        {
            return dbContext.Tags.Find(id);
        }
        public void Create(Tag item)
        {
            dbContext.Tags.Add(item);
        }
        public void Update(Tag item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var deleteTag = dbContext.Tags.Find(id);
            if (deleteTag != null) dbContext.Tags.Remove(deleteTag);
        }
        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return dbContext.Tags.Where(predicate);
        }
    }
}
