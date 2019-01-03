using Lab4.Data.Contracts.Entities;
using System;
using System.Linq;

namespace Lab4.Data.Contracts.Repositories
{
    public interface IContext:IDisposable
    {
        IQueryable<Post> PostsRepository { get; }
        IQueryable<User> UsersRepository { get; }

        void Save();

        void Add<TEntity>(IQueryable<TEntity> repository, TEntity entity)
            where TEntity : class;

    }
}
