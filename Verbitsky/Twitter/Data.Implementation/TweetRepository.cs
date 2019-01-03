using Data.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Implementation
{
    public class TweetRepository
    {
        private readonly ApplicationDbContext context;
        public TweetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(TweetEntity tweet)
        {
            context.Tweets.Add(tweet);
            context.SaveChanges();
        }
        public IEnumerable<TweetEntity> Read()
        {
            var list = context.Tweets.Include(a => a.Author).ToList();
            return list;
        }
        public TweetEntity Read(int id)
        {
            return context.Tweets.Find(id);
        }
        public void Update(TweetEntity tweet)
        {
            context.Tweets.Attach(tweet);
            var entry = context.Entry(tweet);
            entry.Property(a => a.Head).IsModified = true;
            entry.Property(a => a.Content).IsModified = true;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var oldEntity = new TweetEntity { Id = id };
            context.Tweets.Attach(oldEntity);
            context.Tweets.Remove(oldEntity);
            context.SaveChanges();
        }
    }
}
