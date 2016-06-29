using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            int from = 0;
            int to = 0;
            int[] primes = null;

            Console.WriteLine("Please Enter two numbers to compute all primes between them:");
            Console.WriteLine("From: ");
            from = GetInteger();
            Console.WriteLine("To: ");
            to = GetInteger();
            primes = CalcPrimes(from, to);
            Console.WriteLine("The Primes Are: ");
            Console.WriteLine("[{0}]", string.Join(", ", primes));

            Console.ReadKey();
        }

        static int[] CalcPrimes(int from, int to)
        {
            int[] result = null;
            ArrayList collection = new ArrayList();

            if(from < 2)
            {
                from = 2;
            }

            if(from % 2 == 0)
            {
                if(from == 2 && to >= from)
                {
                    collection.Add(2);
                }
                
                from++;
            }

            for (int currentNumber = from; currentNumber <= to; currentNumber += 2)
            {
                if(IsPrime(currentNumber))
                {
                    collection.Add(currentNumber);
                }
            }

            result = new int[collection.Count];
            collection.CopyTo(result);

            return result;
        }

        static bool IsPrime(int number)
        {
            if(number <= 1)
            {
                return false;
            }

            if(number == 2)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if((number % i) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static int GetInteger()
        {
            string input = null;
            bool parsed = false;
            int number = 0;

            while (!parsed)
            {
                input = Console.ReadLine();
                parsed = int.TryParse(input, out number);

                if (!parsed)
                {
                    Console.WriteLine("Wrong input, please enter again!");
                }
            }

            return number;
        }
    }
}
