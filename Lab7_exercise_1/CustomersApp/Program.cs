using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer[] customers =
            {
                new Customer(1, "Avidan Baurchi", "Yavne"),
                new Customer(2, "John Doe", "California"),
                new Customer(3, "Chandler Bean", "New York"),
                new Customer(4, "Darth Vader", "Death Planet")
            };

            Console.WriteLine("{0}Before Sort: {0}", System.Environment.NewLine);
            PrintArray(customers);
            Console.WriteLine("{0}After Sort: {0}", System.Environment.NewLine);
            Array.Sort(customers);
            PrintArray(customers);
            Console.WriteLine();

            Console.WriteLine("Using AnotherCustomerComparer: ");
            Array.Sort(customers, new AnotherCustomerComparer());
            PrintArray(customers);

            Console.ReadKey();
        }

        private static void PrintArray(IEnumerable<object> array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
