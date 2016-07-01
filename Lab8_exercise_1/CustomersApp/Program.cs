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
            ICollection<Customer> filtered = null;
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

            Console.WriteLine("No Filters: (Count: {0})", customers.Length);

            foreach (Customer customer in customers)
            {
                Console.WriteLine(customer);
            }

            filtered = GetCustomers(customers, new CustomerFilter(FilterAToK));
            Console.WriteLine("{0}Filtered A-K: (Count: {1})", System.Environment.NewLine, filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer.ToString());
            }

            filtered = GetCustomers(customers, delegate (Customer customer)
            {
                string name = customer?.Name;
                Regex rgx = new Regex(@"^[l-z].*$", RegexOptions.IgnoreCase);

                if (name != null && name.Length > 0)
                {
                    return rgx.IsMatch(name);
                }

                return false;
            });

            Console.WriteLine("{0}Filtered L-Z: (Count: {1})", System.Environment.NewLine, filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer);
            }

            filtered = GetCustomers(customers, (customer) =>
            {
                return customer != null ? customer.ID < 100 : false;
            });

            Console.WriteLine("{0}Filtered ID < 100: (Count: {1})", System.Environment.NewLine, filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer);
            }

            Console.ReadLine();
        }

        private static bool FilterAToK(Customer customer)
        {
            string name = customer?.Name;
            Regex rgx = new Regex(@"^[a-k].*$", RegexOptions.IgnoreCase);

            if (name != null && name.Length > 0)
            {
                return rgx.IsMatch(name);
            }

            return false;
        }

        private static ICollection<Customer> GetCustomers(ICollection<Customer> customers, CustomerFilter filter)
        {
            List<Customer> filterdCollection = new List<Customer>();

            foreach (Customer customer in customers)
            {
                if (filter != null && filter.Invoke(customer))
                {
                    filterdCollection.Add(customer);
                }
            }

            return filterdCollection;
        }
    }
}
