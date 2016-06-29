using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double number1 = 0d;
            double number2 = 0d;
            double calculationResult = 0d;
            bool isCalculated = false;
            CalculatorOperations operation;

            Console.WriteLine("Please Enter the first number: ");
            number1 = GetDoubleNumber();
            Console.WriteLine("Please Enter a second number: ");
            number2 = GetDoubleNumber();
            Console.WriteLine("Pease enter an operator (+,-,* or /)");
            operation = GetOperator();

            switch (operation)
            {
                case CalculatorOperations.Plus:
                    calculationResult = number1 + number2;
                    isCalculated = true;
                    break;
                case CalculatorOperations.Minus:
                    calculationResult = number1 - number2;
                    isCalculated = true;
                    break;
                case CalculatorOperations.Multiply:
                    calculationResult = number1 * number2;
                    isCalculated = true;
                    break;
                case CalculatorOperations.Divide:
                {
                    if (number2 == 0)
                    {
                        Console.WriteLine("Cannot Divide by Zero !!!");
                    }
                    else
                    {
                        calculationResult = number1 / number2;
                        isCalculated = true;
                    }
                    break;
                }
            }

            if (isCalculated)
            {
                Console.WriteLine("final result: {0}", calculationResult);
            }

            Console.ReadLine();
        }

        static double GetDoubleNumber()
        {
            bool isParsed = false;
            double number = 0d;
            string input = null;

            do
            {
                input = Console.ReadLine();
                isParsed = Double.TryParse(input, out number);

                if (!isParsed)
                {
                    Console.WriteLine("wrong input, please try again..");
                }
            } while (!isParsed);

            return number;
        }

        static CalculatorOperations GetOperator()
        {
            string input = "";
            bool isValidOperator = false;
            CalculatorOperations operation = CalculatorOperations.Plus;

            do
            {
                input = Console.ReadLine();

                switch (input)
                {
                    case "+":
                        operation = CalculatorOperations.Plus;
                        isValidOperator = true;
                        break;
                    case "-":
                        operation = CalculatorOperations.Minus;
                        isValidOperator = true;
                        break;
                    case "*":
                        operation = CalculatorOperations.Multiply;
                        isValidOperator = true;
                        break;
                    case "/":
                        operation = CalculatorOperations.Divide;
                        isValidOperator = true;
                        break;
                    default:
                        isValidOperator = false;
                        Console.WriteLine("Wrond choice, try again!");
                        break;
                }

            } while (!isValidOperator);



            return operation;
        }
    }

    enum CalculatorOperations
    {
        Plus = 0,
        Minus = 1,
        Multiply = 2,
        Divide = 3
    }
}
