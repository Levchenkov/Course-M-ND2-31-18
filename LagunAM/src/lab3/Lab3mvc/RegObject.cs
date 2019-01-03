using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    class RegObject
    {
        public RegObject(Type tResolve, Type tConcrete)
        {
            TResolve = tResolve;
            TConcrete = tConcrete;
        }

        public Type TResolve { get; private set; }

        public Type TConcrete { get; private set; }

        public object Instance { get; private set; }

        public void CreateInstance(params object[] args)
        {
            this.Instance = Activator.CreateInstance(this.TConcrete, args);
        }
    }
}
