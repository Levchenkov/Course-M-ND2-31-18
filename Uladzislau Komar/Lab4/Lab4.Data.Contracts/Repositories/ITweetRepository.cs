using Lab4.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Data.Contracts.Repositories
{
    public interface ITweetRepository
    {
        TweetEntity Create(TweetEntity entity);
        TweetEntity Read(int id);
        List<TweetEntity> Read();
        Task<List<TweetEntity>> ReadAsync();
        void Update(TweetEntity entity);
        void Delete(int id);
    }
}
