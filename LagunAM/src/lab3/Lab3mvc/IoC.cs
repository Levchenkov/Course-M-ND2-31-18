using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3
{
    public class IoC:IMyContainer
    {
        private readonly IList<RegObject> regObjects = new List<RegObject>();


        public void Register<TTypeToResolve, TConcrete>()
        {
            regObjects.Add(new RegObject(typeof(TTypeToResolve), typeof(TConcrete)));

        }

        public object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        private object ResolveObject(Type tResolve)
        {
            var regObject = regObjects.FirstOrDefault(o => o.TResolve == tResolve);
            if (regObject == null)
            {
                throw new Exception(string.Format(tResolve.Name));
            }
            return GetInstance(regObject);
        }

        private object GetInstance(RegObject regObject)
        {
            var parameters = ResolveConstructor(regObject);
            regObject.CreateInstance(parameters.ToArray());
            return regObject.Instance;
        }

        private IEnumerable<object> ResolveConstructor(RegObject regObject)
        {
            var constructorInfo = regObject.TConcrete.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveObject(parameter.ParameterType);
            }
        }

    }
}
