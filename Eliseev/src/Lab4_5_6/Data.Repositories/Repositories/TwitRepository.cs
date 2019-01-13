using Data.Repositories.Contexts;
using Data.Repositories.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Repositories
{
    public class TwitRepository : IRepository<Twit>
    {
        private TwitterIdentityContext db;

        public TwitRepository(TwitterIdentityContext context)
        {
            db = context;
        }

        public Twit Create(Twit item)
        {
            Twit addedTwit = db.Twits.Add(item);
            db.Entry(item).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return addedTwit;
        }

        public IEnumerable<Twit> GetAll()
        {
            return db.Twits.Include("User").ToList<Twit>();
        }
    }
}
