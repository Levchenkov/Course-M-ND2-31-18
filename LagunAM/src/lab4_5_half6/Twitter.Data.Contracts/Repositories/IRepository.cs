using System;
using System.Collections.Generic;
using System.Text;

namespace Twitter.Data.Contracts.Repositories
{
    public interface IRepository<in TKey, TModel>
    {
        TModel Read(TKey id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
    }
}
