using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersApp
{
    public class Customer : IComparable<Customer>, IEquatable<Customer>
    {
        int _id = 0;

        public Customer(int id, string name, string address)
        {
            ID = id;
            Name = name;
            Address = address;
        }

        public string Name { get; set; }

        public int ID
        {
            get { return _id; }
            private set
            {
                if(value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Name", "ID Must be Positive");
                }

                _id = value;
            }
        }

        public string Address { get; set; }

        public int CompareTo(Customer other)
        {
            return (this.Name.ToLower()).CompareTo(other.Name.ToLower());
        }

        public bool Equals(Customer other)
        {
            return this.Name.ToLower() == other.Name.ToLower()
                && this.ID == other.ID;
        }

        public override string ToString()
        {
            return String.Format("Customer:{3}  ID: {0},{3}  Name: {1},{3}  Address: {2}", ID, Name, Address, System.Environment.NewLine);
        }

        public override bool Equals(object obj)
        {
            Customer customer = obj as Customer;

            if(customer != null)
            {
                return Equals(customer);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
