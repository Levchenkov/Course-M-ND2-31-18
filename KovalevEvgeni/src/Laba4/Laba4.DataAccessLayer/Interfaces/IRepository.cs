using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        T GetRecord(int recordId);
        IQueryable<T> Find(Expression<Func<T, Boolean>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int recordId);
    }
}
