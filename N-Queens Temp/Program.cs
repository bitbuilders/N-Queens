using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public int steps;
        public int column;
        public int[] gameBoard;
    }

    class Program
    {
        static List<Solution> solutions;
        static int boardSize = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to N-Queens!\n");
            RestartAlgorithm();
        }

        static void RestartAlgorithm()
        {
            solutions = new List<Solution>();
            boardSize = GetBoardSizeFromUser();
            Console.WriteLine($"\n**n = {boardSize}");
            PlaceAllQueens();
            PrintBoard();
        }

        static void PlaceAllQueens()
        {
            PlaceQueen(0, 0, new int[boardSize]);
            //bool hasSolution = true;
            //int row = 0;
            //while (hasSolution)
            //{
            //    bool placed = PlaceQueen(currentSolution.column, row);
            //    if (placed)
            //    {
            //        currentSolution.column++;
            //        row = 0;
            //    }
            //    else
            //    {
            //        //Console.WriteLine($"Removed queen at [{currentSolution.column}, {row}]");
            //        currentSolution.column--;
            //        row = currentSolution.CurrentRow() + 1;
            //    }

            //    if (currentSolution.column < 0)
            //    {
            //        hasSolution = false;
            //        solutions.Pop();
            //    }
            //    else if (currentSolution.column >= boardSize)
            //    {
            //        if (UniqueSolution(currentSolution))
            //        {
            //            solutions.Push(new Solution(boardSize));
            //            currentSolution = solutions.Peek();
            //        }
            //        else
            //        {
            //            currentSolution.column = currentSolution.column--;
            //            row = currentSolution.CurrentRow() + 1;
            //        }
            //    }
            //}
        }

        static void PlaceQueen(int column, int row, int[] gameBoard)
        {
            for (int i = 0; i < boardSize; ++i)
            {
                gameBoard[column] = i;
                bool valid = QueenIsValid(i, column, gameBoard);
                if (valid)
                {
                    if (column >= boardSize - 1)
                    {
                        if (UniqueSolution(gameBoard))
                        {
                            solutions.Add(new Solution(gameBoard));
                            PlaceQueen(0, 0, new int[boardSize]);
                        }
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

        static bool UniqueSolution(int[] solution)
        {
            bool unique = true;

            foreach (Solution s in solutions)
            {
                if (EqualSolutions(s, solution))
                {
                    unique = false;
                    break;
                }
            }

            return unique;
        }

        static bool EqualSolutions(Solution s1, int[] s2)
        {
            bool equal = true;
            for (int i = 0; i < s1.gameBoard.Length; ++i)
            {
                if (s1.gameBoard[i] != s2[i])
                {
                    equal = false;
                    break;
                }
            }

            return equal;
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
            int count = 1;
            Console.WriteLine($"**Total Solutions: {solutions.Count}\n");
            for (int j = solutions.Count - 1; j >= 0; --j)
            {
                Console.WriteLine($"Solution {count++}:\nSteps taken = {solutions.ElementAt(j).steps}\n");
                int[] gameBoard = solutions.ElementAt(j).gameBoard;
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
                Console.WriteLine();
                Console.WriteLine("------------------------------");
                Console.WriteLine();
            }
        }
    }
}
