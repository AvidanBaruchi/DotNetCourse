using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rationals
{
    class Program
    {
        static void Main(string[] args)
        {
            Rational r1 = new Rational(1, 3);
            Rational r2 = new Rational(1, 5);
            Rational r3 = r1.Add(r2);

            Console.WriteLine(r3.ToString());
            r3 = r3.Add(new Rational(1, 15));
            Console.WriteLine(r3.ToString());
            r3.Reduce();
            Console.WriteLine(r3.ToString());
            r3 = r3.Mul(new Rational(-1, 1));
            Console.WriteLine(r3.ToString());
            Console.WriteLine(new Rational(-9,-15));

            if (r3.Equals(new Rational(3, -5)))
            {
                Console.WriteLine("equals..");
            }

            Console.ReadLine();
        }
    }
}
