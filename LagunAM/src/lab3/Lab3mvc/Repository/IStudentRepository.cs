using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3mvc.Repository
{
    public interface IStudentRepository<TType>
    {
        TType Add(TType model);
        TType Get(TType model);
        TType Remove(TType model);
        TType Update(TType model);

    }
}