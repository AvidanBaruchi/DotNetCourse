using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomersApp
{
    class TestFilters
    {
        private ICollection<Customer> _collction;

        public TestFilters(ICollection<Customer> collection)
        {
            Collection = collection;
        }

        public ICollection<Customer> Collection
        {
            get { return _collction; }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Collection", "Cannot perform test on null objects");
                }
                else
                {
                    _collction = value;
                }
            }    
        }

        public void RunTest()
        {
            Console.WriteLine("No Filters: (Count: {0})", Collection.Count);
            DisplayResults(Collection);
            FilterA_K(Collection);
            Console.WriteLine();
            FilterL_Z(Collection);
            Console.WriteLine();
            FilterID(Collection);
        }

        private void DisplayResults(ICollection<Customer> collection)
        {
            foreach (var customer in collection)
            {
                Console.WriteLine(customer);
            }
        }

        private void FilterID(ICollection<Customer> customers)
        {
            ICollection<Customer> filtered = GetCustomers(customers, (customer) =>
            {
                return customer != null ? customer.ID < 100 : false;
            });

            Console.WriteLine("Filtered ID < 100: (Count: {0})", filtered.Count);
            DisplayResults(filtered);
        }

        private void FilterL_Z(ICollection<Customer> customers)
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
            DisplayResults(filtered);
        }

        private void FilterA_K(ICollection<Customer> customers)
        {
            /*
             * as exercise asks: 
             * filtered = GetCustomers(customers, new CustomerFilter(Program.FilterA_K));
             */
            ICollection<Customer> filtered = GetCustomers(customers,
                CustomerFilterNameFactory(new Regex(@"^[a-k].*$", RegexOptions.IgnoreCase)));
            Console.WriteLine("Filtered A-K: (Count: {0})", filtered.Count);
            DisplayResults(filtered);
        }

        private ICollection<Customer> GetCustomers(ICollection<Customer> customers, CustomerFilter filter)
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

        /// <summary>
        /// A seperate method of filtering A-K as described in 5.b.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private bool FilterA_K(Customer customer)
        {
            string name = customer?.Name;
            Regex rgx = new Regex(@"^[a-k].*$", RegexOptions.IgnoreCase);

            if (name != null && name.Length > 0)
            {
                return rgx.IsMatch(name);
            }

            return false;
        }

        private CustomerFilter CustomerFilterNameFactory(Regex rgx)
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
