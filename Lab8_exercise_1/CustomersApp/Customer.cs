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

        public string Name { get; private set; }

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

        public string Address { get; private set; }

        public int CompareTo(Customer other)
        {
            return string.Compare(Name, other?.Name, true);
        }

        public bool Equals(Customer other)
        {
            if (other == null) return false;

            return string.Equals(Name?.ToLower(), other.Name?.ToLower())
                && ID == other.ID;
        }

        public override string ToString()
        {
            return String.Format("Customer:{3}  ID: {0},{3}  Name: {1},{3}  Address: {2}", ID, Name, Address, System.Environment.NewLine);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Customer);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
