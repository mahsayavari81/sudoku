using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] puzzle = GetSudokuPuzzleFromUser();
            SudokuSolver solver = new SudokuSolver(puzzle);
            int[,] solution = solver.Solve();

            Console.WriteLine("Solution:");
            PrintSudokuBoard(solution);
        }

        static int[,] GetSudokuPuzzleFromUser()
        {
            int[,] puzzle = new int[9, 9];

            Console.WriteLine("Enter the Sudoku puzzle (use 0 for empty cells):");

            for (int row = 0; row < 9; row++)
            {
                string[] input = Console.ReadLine().Split(' ');

                for (int col = 0; col < 9; col++)
                {
                    if (!int.TryParse(input[col], out int value) || value < 0 || value > 9)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid Sudoku puzzle.");
                        return GetSudokuPuzzleFromUser();
                    }

                    puzzle[row, col] = value;
                }
            }

            return puzzle;
        }

        static void PrintSudokuBoard(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Console.Write(board[row, col] + " ");
                }

                Console.WriteLine();
            }
        }
    }

    class SudokuSolver
    {
        private int[,] sudokuBoard;

        public SudokuSolver(int[,] puzzle)
        {
            sudokuBoard = puzzle;
        }

        public int[,] Solve()
        {
            if (SolveSudoku())
            {
                return sudokuBoard;
            }
            else
            {
                Console.WriteLine("No solution exists for the given Sudoku puzzle.");
                Environment.Exit(0);
                return null;
            }
        }

        private bool SolveSudoku()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuBoard[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsNumberSafe(row, col, num))
                            {
                                sudokuBoard[row, col] = num;

                                if (SolveSudoku())
                                {
                                    return true;
                                }

                                sudokuBoard[row, col] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsNumberSafe(int row, int col, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (sudokuBoard[row, i] == num || sudokuBoard[i, col] == num)
                {
                    return false;
                }
            }

            int rw = (row / 3) * 3;
            int cl = (col / 3) * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudokuBoard[rw + i, cl + j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
