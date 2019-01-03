using System;
using System.Collections.Generic;
using System.Linq;
using Twitter.Data.Contracts.Context;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Repositories;

namespace Twitter.Repositories
{
    public class TagRepository : ITagRepository
    {
        private ApplicationDbContext db;

        public TagRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Tag CheckExist(string tagName)
        {
            var item = db.Tags.FirstOrDefault(p => p.Name == tagName);
            return item;
        }

        public void Create(Tag model)
        {
            db.Tags.Add(model);
        }

        public void Delete(Tag model)
        {
            db.Tags.Remove(model);
        }

        public Tag Read(int id)
        {
            var tag = db.Tags.Where(p => p.TagId == id).FirstOrDefault();
            return tag;    
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Tag model)
        {
            throw new NotImplementedException();
        }
    }
}
