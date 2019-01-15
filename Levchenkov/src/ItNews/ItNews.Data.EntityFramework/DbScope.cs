using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using ItNews.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ItNews.Data.EntityFramework
{
    public class DbScope : IDbScope
    {
        private readonly DbContext context;
        private readonly DbScope parent;

        public DbScope(DbContext context)
        {
            this.context = context;
        }

        public DbScope(DbScope parent)
        {
            this.parent = parent;
            context = parent.Context;
        }

        public bool Disposed
        {
            get;
            private set;
        }

        private DbContext Context
        {
            get
            {
                if (parent != null)
                {
                    Contract.Assert(!parent.Disposed, "Parent scope was disposed before the nested scope.");
                    return parent.context;
                }
                return context;
            }
        }

        public void Dispose()
        {
            if (!Disposed && parent == null)
            {
                context.Dispose();
                Disposed = true;
            }
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        //public IQueryable<TEntity> CreateReadOnlyQuery<TEntity>(params string[] includes) where TEntity : Entity
        //{
        //    return SetIncludes(Context.Set<TEntity>().AsNoTracking<TEntity>(), includes);
        //}

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

    }
}