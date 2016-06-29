using System;
using System.Text;

namespace BinaryDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputNumber = 0;
            int sumOfOnes = 0;
            string binaryRepresentation = null;

            Console.WriteLine("Please Enter An Integer: ");
            inputNumber = GetNumber();
            binaryRepresentation = ConvertToBinary(inputNumber);
            Console.WriteLine("Binary Representation: {0}", binaryRepresentation);
            sumOfOnes = countOnes(inputNumber);
            Console.WriteLine("The Number of Ones is: {0}", sumOfOnes);

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

        static string ConvertToBinary(int number)
        {
            string result = "";

            while(number != 0)
            {
                result = (number % 2).ToString() + result;
                number /= 2;
            }

            return result;
        }

        static int countOnes(int number)
        {
            int sum = 0;

            while(number > 0)
            {
                sum += number & 1;
                number = number >> 1;
            }

            return sum;
        }
    }
}
