using Microsoft.EntityFrameworkCore;
using Switter.Data.Contracts.Entities;
using Switter.Data.Contracts.Repositories;
using Switter.Data.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Data.Repositories.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly ApplicationContext context;

        public TweetRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Create(Tweet item)
        {
            context.Tweets.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<Tweet> GetAll()
        {
            return context.Tweets.Include("Author").OrderByDescending(x => x.Date).Take(20);
        }
    }
}
