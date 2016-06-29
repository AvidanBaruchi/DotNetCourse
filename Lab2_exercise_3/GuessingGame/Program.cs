using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int secret = new Random().Next(1, 100);
            int maxNumberOfGuesses = 7;
            int numberOfUserGuesses = 0;
            int guessedNumber = 0;

            for (numberOfUserGuesses = 1; numberOfUserGuesses <= maxNumberOfGuesses; numberOfUserGuesses++)
            {
                Console.WriteLine("Please guess my number: ");
                guessedNumber = GetNumber();

                if (guessedNumber > secret)
                {
                    Console.WriteLine("too big");
                }
                else if (guessedNumber < secret)
                {
                    Console.WriteLine("too small");
                }
                else
                {
                    break;
                }
            }

            if(numberOfUserGuesses > maxNumberOfGuesses)
            {
                Console.WriteLine("You Failed! The Correct number is {0}", secret);
            }
            else
            {
                Console.WriteLine("Great! it took you {0} guesses", numberOfUserGuesses);
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
