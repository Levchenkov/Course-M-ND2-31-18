using System.Linq;
using ItNews.Data.Contracts;
using ItNews.Data.Contracts.Entities;
using ItNews.Domain.Contracts;

namespace ItNews.Domain.Services
{
    public class RatingService : IRatingService
    {
        private readonly IDbScopeFactory scopeFactory;

        public RatingService(IDbScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public int GetRating(long postId)
        {
            using (var scope = scopeFactory.GetSharedScope())
            {
                var summary = scope.CreateQuery<Rating>().Where(x => x.PostId == postId).Sum(x => x.Value);
                return summary;
            }
        }
    }
}