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

            FilterA_K(customers);
            Console.WriteLine();
            FilterL_Z(customers);
            Console.WriteLine();
            FilterID(customers);

            Console.ReadLine();
        }

        private static void FilterID(ICollection<Customer> customers)
        {
            ICollection<Customer> filtered = GetCustomers(customers, (customer) =>
            {
                return customer != null ? customer.ID < 100 : false;
            });

            Console.WriteLine("Filtered ID < 100: (Count: {0})", filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer);
            }
        }

        private static void FilterL_Z(Customer[] customers)
        {
            /*
             * as exercise asks:
             * filtered = GetCustomers(customers, delegate(Customer customer) {
                string name = customer?.Name;
                Regex rgx = new Regex(@"^[l-z].*$", RegexOptions.IgnoreCase);

                if (name != null && name.Length > 0)
                {
                    return rgx.IsMatch(name);
                }

                return false;
            });
             * 
             */
            ICollection<Customer> filtered = GetCustomers(customers,
                            CustomerFilterNameFactory(new Regex(@"^[l-z].*$", RegexOptions.IgnoreCase)));
            Console.WriteLine("Filtered L-Z: (Count: {0})", filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer);
            }
        }

        private static void FilterA_K(Customer[] customers)
        {
            /*
             * as exercise asks: 
             * filtered = GetCustomers(customers, new CustomerFilter(Program.FilterA_K));
             */
            ICollection<Customer> filtered = GetCustomers(customers,
                CustomerFilterNameFactory(new Regex(@"^[a-k].*$", RegexOptions.IgnoreCase)));
            Console.WriteLine("Filtered A-K: (Count: {0})", filtered.Count);

            foreach (Customer customer in filtered)
            {
                Console.WriteLine(customer);
            }
        }

        static ICollection<Customer> GetCustomers(ICollection<Customer> customers, CustomerFilter filter)
        {
            List<Customer> filterdCollection = new List<Customer>();

            foreach (Customer customer in customers)
            {
                if(filter.Invoke(customer))
                {
                    filterdCollection.Add(customer);
                }
            }

            return filterdCollection;
        }

        private static bool FilterA_K(Customer customer)
        {
            string name = null;
            Regex rgx = new Regex(@"^[a-k].*$", RegexOptions.IgnoreCase);

            if (customer != null)
            {
                name = customer.Name;

                if (name != null && name.Length > 0)
                {
                    return rgx.IsMatch(name);
                }
            }

            return false;
        }

        private static CustomerFilter CustomerFilterNameFactory(Regex rgx)
        {
            return (Customer customer) =>
            {
                string name = customer?.Name;

                if (name != null && name.Length > 0)
                {
                    return rgx.IsMatch(name);
                }

                return false;
            };
        }
    }
}
