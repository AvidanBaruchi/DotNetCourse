using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
            var primes = CalcPrimes(1, 30000000).ToList();

            Console.WriteLine($"Found {primes.Count} Results");

            Console.ReadLine();
        }

        public static IEnumerable<int> CalcPrimes(int from, int to)
        {
            var collection = new ConcurrentBag<int>();

            if (from < 2)
            {
                from = 2;
            }

            if (from % 2 == 0)
            {
                if (from == 2 && to >= from)
                {
                    collection.Add(2);
                }

                from++;
            }

            var random = new Random();

            Parallel.For(from, to + 1, (currentNumber, loopState) =>
                {
                    var value = random.Next(10000000);

                    if(value == 0) { loopState.Stop(); }

                    if (IsPrime(currentNumber))
                    {
                        collection.Add(currentNumber);
                    }
                });

            return collection;
        }

        private static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
