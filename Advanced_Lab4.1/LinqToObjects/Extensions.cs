using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjects
{
    public static class Extensions
    {
        public static void CopyTo(this object source, object destination)
        {
            if (source.GetType() != destination.GetType())
            {
                throw new ArgumentException("Must be Same type!", "destination");
            }

            var props =
                 from prop in source.GetType()
                 .GetProperties()
                 where prop.CanRead && prop.CanWrite
                 select prop;

            foreach (var prop in props)
            {
                prop.SetValue(destination, prop.GetValue(source));
            }
        }
    }
}
