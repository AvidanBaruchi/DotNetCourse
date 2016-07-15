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

            //helper.interfaces();
            helper.processes();

            Console.ReadLine();
        }
    }
}
