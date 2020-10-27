using System;

namespace SlotMachine3
{
    class Program
    {
        public enum checkDiag
        {
            leftToRight,
            RightToLeft,
        }
        public enum checkStake
        {
            threeCoins,
            twoCoins,
            oneCoin,
        }
        static void Main(string[] args)
        {
            ///Populates grid with Random numbers 0-2
            int row;
            int col;
            int[,] grid = new int[3, 3];
            var ranNum = new Random();
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    grid[row, col] = ranNum.Next(0, 3);
                    Console.Write($" {grid[row, col]} ");
                    if (col == 2)
                        Console.WriteLine();
                }
            }
        }
        static void populateGrid(int[,] gameGrid, int gridRow, int gridCol)
        {
            Console.Write(gameGrid[gridRow, gridCol]);
            if (gridCol == 2)
                Console.WriteLine();
        }

    }
}
