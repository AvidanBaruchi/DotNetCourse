using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame game = new TicTacToeGame();
            Position playerMove = new Position(0, 0);
            bool validMove = false;

            while (!game.IsGameOver)
            {
                Console.WriteLine("Player's {0} Turn", game.Turn);
                game.DisplayBoard();
                validMove = false;

                do
                {
                    playerMove = GetMove();
                    validMove = game.MakeMove(playerMove);

                    if (!validMove)
                    {
                        Console.WriteLine("Not a Valid Move!");
                    }
                } while (!validMove);

                Console.Clear();
            }

            game.DisplayBoard();

            if(game.Winner != TicTacToeOptions.Empty)
            {
                Console.WriteLine("Player {0} Wins!", game.Turn);
            }
            else
            {
                Console.WriteLine("Its a Tie!");
            }

            Console.ReadLine();
        }

        static Position GetMove()
        {
            bool isParsed = false;
            string input = "";
            int row = 0;
            int column = 0;
            Position position = new Position();

            Console.WriteLine("Please choose a position (A1, B2 ...)");

            while (!isParsed)
            {
                input = Console.ReadLine();

                if(input.Length != 2 || !char.IsUpper(input[0]) || !char.IsDigit(input[1]))
                {
                    Console.WriteLine(
@"Error: This is not a position on the board! 
A position on the board must start with a Capital letter and then a number!");
                }
                else
                {
                    row = int.Parse(input[1].ToString());
                    row--;
                    column = input[0] - 'A';
                    isParsed = true;
                    position = new Position(row, column);
                }
            }

            return position;
        }
    }
}
