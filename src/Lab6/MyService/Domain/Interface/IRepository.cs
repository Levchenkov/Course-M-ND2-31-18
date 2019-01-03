using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4.MyService.Domain.Interface
{
    interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}