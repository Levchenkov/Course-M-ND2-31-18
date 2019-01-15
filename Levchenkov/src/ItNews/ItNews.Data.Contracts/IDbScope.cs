using System;
using System.Linq;

namespace ItNews.Data.Contracts
{
    public interface IDbScope : IDisposable
    {
        bool Disposed
        {
            get;
        }

        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}