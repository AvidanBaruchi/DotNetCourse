using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AttribDemo.Classes;

namespace AttribDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AnalayzeAssembly(Assembly.GetExecutingAssembly());
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine($"Could not acces assembly types: {e.Message}");
            }
        }

        public static bool AnalayzeAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            IEnumerable<CodeReviewAttribute> reviewAttributes = null;

            foreach (var type in types)
            {
                if (type.IsDefined(typeof(CodeReviewAttribute), false))
                {
                    reviewAttributes = type.GetCustomAttributes<CodeReviewAttribute>();

                    if (reviewAttributes.ToList().Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        foreach (var item in reviewAttributes)
                        {
                            if (item.IsApproved == true) {

                            }
                        }
                    }
                }
                else {
                    return false;
                }
            }

            return true;
        }
    }
}
