using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switter.Data.Contracts.Repositories
{
    public interface IRepository<T>
    {
        void Create(T item);
        IEnumerable<T> GetAll();
    }
}
