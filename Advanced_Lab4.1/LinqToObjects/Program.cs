using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();

            Console.WriteLine("Interfaces");
            helper.interfaces();
            Console.WriteLine("Processes");
            helper.processes();
            Console.WriteLine("Threads");
            helper.threads();
            Console.WriteLine("CopyTo");
            helper.CopyToMethodTest();

            Console.ReadLine();
        }
    }
}
