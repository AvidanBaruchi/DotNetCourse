using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarStairs
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputNumber = 0;

            Console.WriteLine("Please Enter A Number: ");
            inputNumber = GetNumber();

            for (int i = 1; i <= inputNumber; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write("$");
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }

        static int GetNumber()
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
