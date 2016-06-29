using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quad
{
    class Program
    {
        /// <summary>
        /// Calculates the equation a*x^2+b*x+c=0 from cmd line args.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            bool isParsed = false;   

            if(args.Length == 3)
            {
                isParsed = Double.TryParse(args[0], out a) &&
                    Double.TryParse(args[1], out b) &&
                    Double.TryParse(args[2], out c);

                if (isParsed)
                {
                    Calculate(a, b, c);
                }
                else
                {
                    Console.WriteLine("one or more arguments are not numbers!");
                }
            }
            else
            {
                Console.WriteLine("should be entered 3 number arguments!");
            }

            Console.ReadLine();
        }

        static void Calculate(double a, double b, double c)
        {
            double delta = 0;
            double deltaSquare = 0;
            double solution1 = 0;
            double solution2 = 0;

            if (a != 0)
            {
                delta = (b * b) - (4 * a * c);

                if (delta < 0)
                {
                    Console.WriteLine("No solution to the equation!");
                }
                else
                {
                    deltaSquare = Math.Sqrt(delta);
                    solution1 = ((-b) + deltaSquare) / 2*a;
                    solution2 = ((-b) - deltaSquare) / 2*a;

                    if (delta == 0)
                    {
                        Console.WriteLine("One Solution: x1 = {0}", solution1);
                    }
                    else
                    {
                        Console.WriteLine("Two Solutions: x1 = {0}, x2 = {1}", solution1, solution2);
                    }
                }  
            }
            else if (b != 0)
            {
                Console.WriteLine("Not a Quadratic equation!");
            }
        }
    }
}
