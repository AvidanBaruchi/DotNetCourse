using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToeGame
    {
        private int _boardSize = 3;

        public TicTacToeGame()
        {
            Board = new TicTacToeOptions[_boardSize, _boardSize];
            Turn = TicTacToeOptions.X;
            IsGameOver = false;
            Winner = TicTacToeOptions.Empty;
        }

        public TicTacToeOptions[,] Board { get; private set; }

        public bool IsGameOver { get; private set; }

        public TicTacToeOptions Turn { get; private set; }

        public TicTacToeOptions Winner { get; private set; }

        public void DisplayBoard()
        {
            StringBuilder builder = new StringBuilder();
            TicTacToeOptions currentOption;

            builder.Append(String.Format("{0,-3}", " "));
            builder.Append(String.Format("{0}", "A"));
            builder.Append(String.Format("{0,4}", "B"));
            builder.Append(String.Format("{0,4}", "C"));
            builder.AppendLine();

            for (int i = 0; i < _boardSize; i++)
            {
                builder.Append('=', 2 + (4 * _boardSize));
                builder.AppendLine();
                builder.Append(String.Format("{0,-3}", (i + 1) + "|"));
                
                for (int j = 0; j < _boardSize; j++)
                {
                    currentOption = Board[i, j];

                    if (currentOption != TicTacToeOptions.Empty)
                    {
                        builder.Append(String.Format("{0,-4}", currentOption + " |"));
                    }
                    else
                    {
                        builder.Append(String.Format("{0,-4}", "  |"));
                    }   
                }

                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
        }

        public bool MakeMove(Position position)
        {
            bool isValid = IsLegalMove(position);

            if(isValid)
            {
                Board[position.X, position.Y] = Turn;
                ComputeIsGameOver();

                if(!IsGameOver)
                {
                    Turn = Turn == TicTacToeOptions.X ? TicTacToeOptions.O : TicTacToeOptions.X;
                }
            }

            return isValid;
        }

        private bool IsLegalMove(Position position)
        {
            bool isValid = false;

            if (!IsGameOver)
            {
                if (position.X < _boardSize && position.X >= 0)
                {
                    if (position.Y < _boardSize && position.Y >= 0)
                    {
                        if (Board[position.X, position.Y] == TicTacToeOptions.Empty)
                        {
                            isValid = true;
                        }
                    }
                } 
            }

            return isValid;
        }

        private void ComputeIsGameOver()
        {
            int isAllMarkedCounter = 0;
            bool isWin = false;
            int rowCounter = 0;
            int columnCounter = 0;
            int diagonalCounter = 0;
            int counterDiagonalCounter = 0;

            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if(Board[i, j] == Turn)
                    {
                        rowCounter++;
                    }

                    if(Board[j, i] == Turn)
                    {
                        columnCounter++;
                    }

                    if(Board[i, j] != TicTacToeOptions.Empty)
                    {
                        isAllMarkedCounter++;
                    }
                }

                if(Board[i, i] == Turn)
                {
                    diagonalCounter++;
                }

                if(Board[_boardSize - i - 1, i] == Turn)
                {
                    counterDiagonalCounter++;
                }

                if (rowCounter == _boardSize || columnCounter == _boardSize || diagonalCounter == _boardSize
                    || counterDiagonalCounter == _boardSize)
                {
                    isWin = true;
                    break;
                }
                else
                {
                    rowCounter = 0;
                    columnCounter = 0;
                }
            }

            if(isWin)
            {
                Winner = Turn;
                IsGameOver = true;
            }
            else if(isAllMarkedCounter == (_boardSize * _boardSize))
            {
                IsGameOver = true;
                Winner = TicTacToeOptions.Empty;
            }
        }
    }
}
