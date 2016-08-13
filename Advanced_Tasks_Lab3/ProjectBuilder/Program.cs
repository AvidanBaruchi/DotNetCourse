using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new Helper();

            Console.WriteLine("Sequentialy");
            helper.BuildProjectsSequentialy();
            Console.ReadLine();
            Console.WriteLine("Concurrently");
            helper.BuildProjectsConcurrently();

            Console.ReadLine();
        }
    }
}
