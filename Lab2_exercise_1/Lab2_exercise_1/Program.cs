using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloPerson
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = null;
            int number = 0;
            string numberString = null;
            bool isParsed = false;

            Console.WriteLine("What’s your name?");
            userName = Console.ReadLine();
            Console.WriteLine("Hello, {0}", userName);
            Console.WriteLine("Please enter a number between 1-10");
            numberString = Console.ReadLine();
            isParsed = int.TryParse(numberString, out number);

            if (isParsed && (number >= 0 || number <= 10))
            {
                for (var spacesNumber = 0; spacesNumber < number; spacesNumber++)
                {
                    for (var j = 0; j < spacesNumber; j++)
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine(userName);
                } 
            }
            else
            {
                Console.WriteLine("You did not entered a number as specified! GoodBye :)");
            }
        }
    }
}
