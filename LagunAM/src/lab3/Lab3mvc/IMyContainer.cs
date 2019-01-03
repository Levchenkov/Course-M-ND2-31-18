using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    public interface IMyContainer
    {
        void Register<TResolve, TConcrete>();
        object Resolve(Type typeToResolve);
    }
}
