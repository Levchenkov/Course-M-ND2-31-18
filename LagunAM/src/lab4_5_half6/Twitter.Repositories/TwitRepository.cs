using System;
using System.Collections.Generic;
using System.Text;
using Twitter.Data.Contracts.Context;
using Twitter.Data.Contracts.Entities;
using Twitter.Data.Contracts.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Twitter.Repositories
{
    public class TwitRepository : ITwitRepository
    {
        private ApplicationDbContext db;

        public TwitRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(Twit model)
        {
            db.Twits.Add(model);
        }

        public void Delete(Twit model)
        {
            db.Twits.Remove(model);
        }

        public IEnumerable<Twit> GetPack(int size)
        {

            var items = db.Twits.OrderByDescending(p => p.TwitId).Take(size).ToList();
            return items;
        }

        public Twit Read(int id)
        {
            var twit = db.Twits.Where(p => p.TwitId == id).FirstOrDefault();
            return twit;
        }

        public void Update(Twit model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void TwitTagSave(Twit twit, IEnumerable<Tag> tags)
        {
            foreach (Tag item in tags)
            {
                TwitTag twitTag = new TwitTag{ TagId = item.TagId, Tag = item,
                    Twit = twit, TwitId = twit.TwitId};
                db.TwitTags.Add(twitTag);
            }
        }

        public string GetTagsString(Twit twit)
        {
            string result = "";
            var items = db.TwitTags.Where(p => p.TwitId == twit.TwitId);
            foreach (var item in items)
            {
                var tag = db.Tags.Where(p => p.TagId == item.TagId).FirstOrDefault();
                result += tag.Name + " ";
            }
            return result;
        }
    }
}
