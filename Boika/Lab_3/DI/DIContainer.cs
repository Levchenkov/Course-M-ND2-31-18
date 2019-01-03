using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DI
{
    public class DIContainer 
    {
        private List<Tuple<Type, Type>> RegisterList;

        public DIContainer()
        {
            RegisterList = new List<Tuple<Type, Type>>();
        }

        public void Register(Type type1, Type type2)
        {
            RegisterList.Add(new Tuple<Type, Type>(type1, type2));
        }

        public object Create(Type type)
        {
            var register = RegisterList.Where(a => a.Item1 == type).Select(a => a.Item2).Single();
            return Activator.CreateInstance(register, GetParams(register));
        }

        private object[] GetParams(Type register)
        {
            var paramsConstructor = register.GetConstructors().First().GetParameters();
            return paramsConstructor.Select(a => Create(a.ParameterType)).ToArray();
        }
    }
}