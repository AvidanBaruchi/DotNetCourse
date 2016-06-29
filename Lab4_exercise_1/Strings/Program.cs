using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            string[] words = null;
            string[] reversed = null;
            string[] sorted = null;

            while (true)
            {
                Console.WriteLine("{0}Please Enter a Sentence", System.Environment.NewLine);
                input = Console.ReadLine();

                if (input == string.Empty)
                {
                    Console.WriteLine("No Sentence was inserted, Goodbye! :)");
                    break;
                }

                words = input.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("The Number of Words is: {0}", words.Length);
                reversed = words.Reverse<string>().ToArray();
                Console.WriteLine("Reversed Sentence: {0}", JoinArray(reversed, " "));
                sorted = new string[words.Length];
                words.CopyTo(sorted, 0);
                Array.Sort(sorted);
                Console.WriteLine("Sorted Sentence: {0}", JoinArray(sorted, " "));
            }

            Console.ReadLine();
        }

        private static string JoinArray(string[] array, string delimiter)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in array)
            {
                builder.Append(item);
                builder.Append(delimiter);
            }

            return builder.ToString();
        }
    }
}
