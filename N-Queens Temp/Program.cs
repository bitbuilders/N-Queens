using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Queens_Temp
{
    class Program
    {
        static int[] gameBoard;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to N-Queens!\n");
            RestartAlgorithm();
        }

        static void RestartAlgorithm()
        {
            gameBoard = new int[GetBoardSizeFromUser()];
            PlaceAllQueens();
            PrintBoard();
        }

        static void PlaceAllQueens()
        {
            bool queensToPlace = true;

            while (queensToPlace)
            {

            }
        }

        static bool PlaceQueen(int colum, int row)
        {
            bool isValid = true;

            gameBoard[colum] = row;

            if (colum > 0)
            {
                for (int i = colum - 1; i >= 0; --i)
                {
                    int checkIndexBelow = (colum - i) + row;
                    int checkIndexAbove = (colum - i) - row;
                    if (gameBoard[i] == row || gameBoard[i] == checkIndexAbove || gameBoard[i] == checkIndexAbove)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        static int GetBoardSizeFromUser()
        {
            Console.WriteLine("Enter in a board size:");
            bool validInput = false;
            int size = 0;
            do
            {
                string input = Console.ReadLine();
                if (!int.TryParse(input, out size))
                {
                    Console.WriteLine("Whoops, that's not an integer. Please enter a number:");
                }
                else if (size <= 0)
                {
                    Console.WriteLine("How can you have a board size like that? Try entering in a positive number:");
                }
                else
                {
                    validInput = true;
                }
            } while (!validInput);

            return size;
        }

        static void PrintBoard()
        {
            int size = gameBoard.Length;
            for (int i = 0; i < size * size; ++i)
            {
                int y = i / size;
                int x = i % size;
                if (x == 0 && i != 0)
                {
                    Console.WriteLine();
                }
                if (y == gameBoard[x])
                {
                    Console.Write("|Q|");
                }
                else
                {
                    Console.Write("| |");
                }
            }
            Console.WriteLine();
        }
    }
}
