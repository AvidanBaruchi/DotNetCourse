using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjects
{
    /// <summary>
    /// A Class similiar to TestClass with a bit differeces.
    /// GetAndSet do not have a set anymore.
    /// </summary>
    class AnotherTestClass
    {
        private int _someField = 0;
        private int _getAndSet = 0;

        public AnotherTestClass(int readOnlyPropValue)
        {
            ReadOnlyProp = readOnlyPropValue;
            _someField = readOnlyPropValue;
            _getAndSet = readOnlyPropValue;
        }

        public int GetAndSet { get { return _getAndSet; } }

        public int ReadOnlyProp { get; private set; }

        public int NoSetProp => _someField;

        public int SomeMethod()
        {
            return 4;
        }

        public override string ToString()
        {
            return $"GetAndSet: {GetAndSet}, ReadOnlyProp: {ReadOnlyProp}, NoSetProp: {NoSetProp}";
        }
    }
}
