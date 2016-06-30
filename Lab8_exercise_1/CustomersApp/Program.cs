using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFilters test = null;
            Customer[] customers = 
            {
                new Customer(1, "Avidan Baurchi", "Yavne"),
                new Customer(2, "John Doe", "California"),
                new Customer(73, "Chandler Bean", "New York"),
                new Customer(400, "Darth Vader", "Death Planet"),
                new Customer(5, "Klara Lara", "World"),
                new Customer(6, "Tim Benrers Lee", "WWW"),
                new Customer(100, "Victor v", "Here"),
                null
            };

            test = new TestFilters(customers);
            test.RunTest();

            Console.ReadLine();
        }
    }
}
