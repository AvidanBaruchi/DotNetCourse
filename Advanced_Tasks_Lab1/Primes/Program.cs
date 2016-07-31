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
            Stopwatch stopwatch = null;
            IEnumerable<int> primes = null;

            stopwatch = Stopwatch.StartNew();
            primes = CalcPrimesNoParallel(1, 5000000);
            stopwatch.Stop();
            Console.WriteLine($"No parallel took {stopwatch.ElapsedMilliseconds}ms");

            for (int i = 1; i <= 8; i*=2)
            {
                stopwatch = Stopwatch.StartNew();
                primes = CalcPrimes(1, 5000000, i);
                stopwatch.Stop();
                Console.WriteLine($"max degree {i} took {stopwatch.ElapsedMilliseconds}ms");
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static IEnumerable<int> CalcPrimes(int from, int to, int maximumDegreeOfParallelism)
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

            Parallel.For(from, to + 1, new ParallelOptions() { MaxDegreeOfParallelism = maximumDegreeOfParallelism },
                currentNumber =>
                {
                    if (IsPrime(currentNumber))
                    {
                        collection.Add(currentNumber);
                    }
                });

            return collection;
        }

        public static IEnumerable<int> CalcPrimesNoParallel(int from, int to)
        {
            List<int> collection = new List<int>();

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

            for (int currentNumber = from; currentNumber <= to; currentNumber += 2)
            {
                if (IsPrime(currentNumber))
                {
                    collection.Add(currentNumber);
                }
            }

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
