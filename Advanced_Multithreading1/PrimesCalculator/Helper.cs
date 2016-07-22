using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    class Helper
    {
        public int[] CalcPrimes(int from, int to)
        {
            int[] result = null;
            ArrayList collection = new ArrayList();

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

            result = new int[collection.Count];
            collection.CopyTo(result);

            return result;
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
