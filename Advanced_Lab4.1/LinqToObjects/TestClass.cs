using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjects
{
    class TestClass
    {
        private int _someField = 0;

        public TestClass(int readOnlyPropValue)
        {
            ReadOnlyProp = readOnlyPropValue;
            _someField = readOnlyPropValue;
        }

        public int GetAndSet { get; set; }

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
