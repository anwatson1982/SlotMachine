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

            int row;
            int col;
            int[,] grid = new int[3, 3];
            var ranNum = new Random();
            int stack = 20;
            ///Displays welcome message with rules of the game and displays opening ammount of coins
            welcome(stack);
            string playGame = Console.ReadLine();
            Console.Clear();
            for (stack = 20; stack > 0;)
            {
                coinDisplay(stack);
                ///Populates grid with random numbers from 0 - 2 
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
        }

        /// <summary>
        /// Function to dispay ammount of coins users has 
        /// </summary>
        /// <param name="coins">Ammount of coins user left in stack</param>
        static void coinDisplay(int coins)
        {
            Console.WriteLine($"******************");
            Console.WriteLine($"You have {coins} left");
            Console.WriteLine($"******************");
        }
        /// <summary>
        /// Function for Welcome display shows users the rules of the game
        /// </summary>
        static void welcomDisplay()
        {
            Console.WriteLine($"Enter how many coins you want to gamble (max of 3 coins)");
            Console.WriteLine($"If you gamble 3 coins you win on ALL Horizontal lins and Diagnal lins");
            Console.WriteLine($"If you gamble 2 coins you can win on ALL Hozizontal lines");
            Console.WriteLine($"If you gamble 1 coin you can win on just the middle Hozizontal line");
        }
        static void welcome(int coins)
        { 
            welcomDisplay();
            coinDisplay(coins);
        }
    }
}
