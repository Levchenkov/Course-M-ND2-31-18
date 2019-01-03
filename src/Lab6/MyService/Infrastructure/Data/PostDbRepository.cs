using Lab4.Models;
using Lab4.MyService.Domain.Interface;
using Lab4.MyService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab4.MyService.Infrastructure.Data
{
    public class PostDbRepository : IRepository<Post>
    {
        DataContext db;
        public PostDbRepository()
        {
            db = new DataContext();
        }

        public void Create(Post item)
        {
            db.Posts.Add(item);
        }

        public void Delete(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts.ToList().Take(5).OrderByDescending(s=>s.Created);
            
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Post item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}