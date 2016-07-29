using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    class Helper
    {
        public IEnumerable<int> CalcPrimes(int from, int to)
        {
            return CalcPrimesCancelable(from, to, null);
        }

        public IEnumerable<int> CalcPrimesCancelable(int from, int to, WaitHandle handle)
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
                if (handle != null 
                    && handle.WaitOne(0))
                {
                    break;
                }

                if (IsPrime(currentNumber))
                {
                    collection.Add(currentNumber);
                }
            }

            return collection;
        }

        private bool IsPrime(int number)
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
