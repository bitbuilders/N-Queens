using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace N_Queens
{
    class Solution
    {
        public Solution(int[] board)
        {
            steps = 0;
            column = 0;
            gameBoard = board;
        }

        public int CurrentRow()
        {
            if (column >= 0 && column < gameBoard.Length)
                return gameBoard[column];
            else
                return 0;
        }

        public long steps;
        public int column;
        public int[] gameBoard;
    }

    class Program
    {
        static Stopwatch sw;
        static List<Solution> solutions;
        static int boardSize = 0;
        static long steps = 0;
        static long solutionCount = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to N-Queens!\n");
            sw = new Stopwatch();
            RestartAlgorithm();
        }

        static void RestartAlgorithm()
        {
            solutions = new List<Solution>();
            boardSize = GetBoardSizeFromUser();
            Console.WriteLine($"\n**n = {boardSize}");
            sw.Start();
            PlaceAllQueens();
            sw.Stop();
            PrintBoard();
            Console.WriteLine();
            //Console.WriteLine($"{solutionCount} solutions in {sw.ElapsedMilliseconds / 1000} seconds ({sw.ElapsedMilliseconds} millis)\n\n");
            Console.WriteLine($"Took {sw.ElapsedMilliseconds / 1000} seconds ({sw.ElapsedMilliseconds} millis)\n\n");
        }

        static void PlaceAllQueens()
        {
            PlaceQueen(0, 0, new int[boardSize]);
        }

        static void PlaceQueen(int column, int row, int[] gameBoard)
        {
            for (int i = 0; i < boardSize; ++i)
            {
                ++steps;
                gameBoard[column] = i;
                bool valid = QueenIsValid(i, column, gameBoard);
                if (valid)
                {
                    if (column >= boardSize - 1)
                    {
                        //++solutionCount;
                        Solution solution = new Solution(gameBoard);
                        solution.steps = steps;
                        solutions.Add(solution);
                        break;
                    }
                    else
                    {
                        PlaceQueen(column + 1, 0, (int[])gameBoard.Clone());
                    }
                }
            }
        }

        static bool QueenIsValid(int row, int column, int[] gameBoard)
        {
            bool isValid = true;

            if (column > 0)
            {
                for (int i = column - 1; i >= 0; --i)
                {
                    int checkIndexBelow = (column - i) + row;
                    int checkIndexAbove = row - (column - i);
                    if (gameBoard[i] == row || gameBoard[i] == checkIndexAbove || gameBoard[i] == checkIndexBelow)
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
            StringBuilder sb = new StringBuilder();
            sb.Append("**Total Solutions: ");
            sb.Append(solutions.Count);
            sb.Append("\n\n");
            //Console.WriteLine($"**Total Solutions: {solutions.Count}\n");
            for (int j = 0; j < solutions.Count; ++j)
            {
                sb.Append("Solution ");
                sb.Append(solutionCount++);
                sb.Append(":\n");
                sb.Append("Steps Taken = ");
                sb.Append(solutions.ElementAt(j).steps);
                sb.Append("\n\n");
                //Console.WriteLine($"Solution {count++}:\nSteps taken = {solutions.ElementAt(j).steps}\n");
                int[] gameBoard = solutions.ElementAt(j).gameBoard;
                int size = gameBoard.Length;
                for (int i = 0; i < size * size; ++i)
                {
                    int y = i / size;
                    int x = i % size;
                    if (x == 0 && i != 0)
                    {
                        sb.Append("\n");
                        //Console.WriteLine();
                    }
                    if (y == gameBoard[x])
                    {
                        sb.Append("|Q|");
                        //Console.Write("|Q|");
                    }
                    else
                    {
                        sb.Append("| |");
                        //Console.Write("| |");
                    }
                }
                sb.Append("\n\n--------------------------\n\n");
                //Console.WriteLine();
                //Console.WriteLine();
                //Console.WriteLine("------------------------------");
                //Console.WriteLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
