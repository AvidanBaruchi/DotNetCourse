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
            var query =
                from prop1 in source.GetType().GetProperties()
                where prop1.CanRead
                from prop2 in destination.GetType().GetProperties()
                where prop2.CanWrite
                where prop1.Name == prop2.Name
                select new
                {
                    Src = prop1,
                    Dest = prop2
                };


            foreach (var pair in query)
            {
                pair.Dest.SetValue(destination, pair.Src.GetValue(source));
            }
        }
    }
}
