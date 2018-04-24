using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Queens_Temp
{
    class Solution
    {
        public Solution(int boardSize)
        {
            steps = 0;
            column = 0;
            gameBoard = new int[boardSize];
        }

        public int CurrentRow()
        {
            return gameBoard[column];
        }

        public int steps;
        public int column;
        public int[] gameBoard;
    }

    class Program
    {


        static Stack<Solution> solutions;
        static int boardSize = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to N-Queens!\n");
            RestartAlgorithm();
        }

        static void RestartAlgorithm()
        {
            solutions = new Stack<Solution>();
            boardSize = GetBoardSizeFromUser();
            PlaceAllQueens();
            PrintBoard();
        }

        static void PlaceAllQueens()
        {
            solutions.Push(new Solution(boardSize));

            Solution currentSolution = solutions.Peek();
            bool hasSolution = true;

            int row = 0;
            while (hasSolution)
            {
                bool placed = PlaceQueen(currentSolution.column, row);
                if (placed)
                {
                    currentSolution.column++;
                    row = 0;
                }
                else
                {
                    currentSolution.column--;
                    row = currentSolution.CurrentRow() + 1;
                }

                if (currentSolution.column >= currentSolution.gameBoard.Length)
                {
                    if (UniqueSolution(currentSolution))
                    {
                        hasSolution = false;
                        //solutions.Push(new Solution(boardSize));
                        currentSolution = solutions.Peek();
                        row = 0;
                    }
                    else
                    {
                        solutions.Pop();
                        hasSolution = false;
                    }
                }
                else if (currentSolution.column < 0)
                {
                    solutions.Pop();

                    hasSolution = false;
                }
            }
        }

        static bool PlaceQueen(int column, int row)
        {
            bool placedQueen = true;
            Solution solution = solutions.Peek();
            int[] gameBoard = solution.gameBoard;
            solution.steps++;
            
            if (column >= gameBoard.Length || row >= gameBoard.Length)
            {
                return false;
            }
            gameBoard[column] = row;
            bool validPlacement = QueenIsValid(row, column);
            int currentRow = row;
            while (!validPlacement)
            {
                if (currentRow >= gameBoard.Length)
                {
                    placedQueen = false;
                    break;
                }

                solution.steps++;
                currentRow++;
                gameBoard[column] = currentRow;
                validPlacement = QueenIsValid(currentRow, column);
            }
            if (placedQueen)
                Console.WriteLine($"Placed queen at [{column}, {currentRow}]");

            return placedQueen;
        }

        static bool QueenIsValid(int row, int column)
        {
            int[] gameBoard = solutions.Peek().gameBoard;
            bool isValid = true;

            if (column > 0)
            {
                for (int i = column - 1; i >= 0; --i)
                {
                    int checkIndexBelow = (column - i) + row;
                    int checkIndexAbove = (column - i) - row;
                    if (gameBoard[i] == row || gameBoard[i] == checkIndexAbove || gameBoard[i] == checkIndexAbove)
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }

        static bool UniqueSolution(Solution solution)
        {
            bool unique = true;

            foreach (Solution s in solutions)
            {
                if (s != solution)
                {
                    if (EqualSolutions(s, solution))
                    {
                        unique = false;
                        break;
                    }
                }
            }

            return unique;
        }

        static bool EqualSolutions(Solution s1, Solution s2)
        {
            bool equal = true;
            for (int i = 0; i < s1.gameBoard.Length; ++i)
            {
                if (s1.gameBoard[i] != s2.gameBoard[i])
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
            int[] gameBoard = solutions.Peek().gameBoard;
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
