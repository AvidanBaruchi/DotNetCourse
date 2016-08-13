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
        public async Task<int> CountPrimesAsync(int from, int to, WaitHandle handle)
        {
            return await Task.Run(() =>
            {
                int count = 0;

                PrimesMechanism(from, to, handle, _ => { count++; });

                return count;
            });   
        }

        public async Task<IEnumerable<int>> CalcPrimesAsync(int from, int to, WaitHandle handle)
        {
            return await Task.Run(() => CalcPrimesCancelable(from, to, handle));
        }
        
        public IEnumerable<int> CalcPrimes(int from, int to)
        {
            return CalcPrimesCancelable(from, to, null);
        }

        public IEnumerable<int> CalcPrimesCancelable(int from, int to, WaitHandle handle)
        {
            List<int> collection = new List<int>();

            PrimesMechanism(from, to, handle, prime => collection.Add(prime));

            return collection;
        }

        private void PrimesMechanism(int from, int to, WaitHandle handle, Action<int> onPrimeFound)
        {
            if (onPrimeFound == null)
            {
                throw new ArgumentNullException(nameof(onPrimeFound), "action must be a valid delegate!");
            }

            if (from < 2)
            {
                from = 2;
            }

            if (from % 2 == 0)
            {
                if (from == 2 && to >= from)
                {
                    onPrimeFound(2);
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
                    onPrimeFound(currentNumber);
                }
            }
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
