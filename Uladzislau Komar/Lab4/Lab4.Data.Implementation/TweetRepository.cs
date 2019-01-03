using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Data.Contracts.Entities;
using Lab4.Data.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Implementation
{
    public class TweetRepository : ITweetRepository
    {
        private readonly Lab4DbContext context;

        public TweetRepository(Lab4DbContext context)
        {
            this.context = context;
        }

        public TweetEntity Create(TweetEntity entity)
        {
            context.LabTweets.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = new TweetEntity { TweetId = id };
            context.LabTweets.Attach(entity);
            context.LabTweets.Remove(entity);
            context.SaveChanges();
        }

        public TweetEntity Read(int id)
        {
            var output = context.LabTweets.Find(id);
            return output;
        }

        public List<TweetEntity> Read()
        {
            var output = context.Set<TweetEntity>().Include(x => x.Author).ToList();
            return output;
        }

        public Task<List<TweetEntity>> ReadAsync()
        {
            var output = context.Set<TweetEntity>().ToListAsync();
            return output;
        }

        public void Update(TweetEntity entity)
        {
            context.LabTweets.Attach(entity);
            var entry = context.Entry(entity);
            entry.Property(e => e.Content).IsModified = true;
            context.SaveChanges();
        }
    }
}
