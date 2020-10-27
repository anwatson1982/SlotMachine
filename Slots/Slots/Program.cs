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
        
            Console.Clear();
            for (stack = 20; stack > 0;)
            {
                coinDisplay(stack);
                ///Enter stake and uses totalCoins function to remove coins from coin stack
                Console.WriteLine($"Enter stake you would like to play (1,2 or 3 coins)");
                string playGame = Console.ReadLine();      
                int inputStake = Int32.Parse(playGame);
                Console.Clear();            
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
                bool topLine = checkHorizontal(grid, 0);
                bool midLine = checkHorizontal(grid, 1);
                bool botLine = checkHorizontal(grid, 2);
                if (topLine || midLine || botLine == true)
                {
                    Console.WriteLine($"You win =)");
                }
                else
                {
                    Console.WriteLine($"You lose =(");
                }
                    if (inputStake == 1)
                {
                    stack = totalCoins(stack, checkStake.oneCoin);
                   // stack = calcWinnings(inputStake, checkStake.oneCoin);
                    
                }
                if (inputStake == 2)
                {
                    stack = totalCoins(stack, checkStake.twoCoins);
                   // stack = calcWinnings(inputStake, checkStake.twoCoins);

                }
                if (inputStake == 3)
                {
                    stack = totalCoins(stack, checkStake.threeCoins);
                    stack = calcWinnings(inputStake, checkStake.threeCoins);

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
        /// <summary>
        /// This works out stake and how much to take from coin stack depending on which stake is placed
        /// </summary>
        /// <param name="coins">Ammount of coins</param>
        /// <param name="checking">which amount of coins to take away from coin stack (1,2 or 3)</param>
        /// <returns>Total number of coins left after stake has been placed</returns>
        static int totalCoins(int coins, checkStake checking)
        {
            if (checking == checkStake.oneCoin)
            {
                int result = coins - 1;
                return result;
            }
            if (checking == checkStake.twoCoins)
            {
                int result = coins - 2;
                return result;
            }
            if (checking == checkStake.threeCoins)
            {
                int result = coins - 3;
                return result;
            }
            else
                return 0;
        }
        static bool checkHorizontal(int[,] gameGrid, int winRow)
        {
            if (gameGrid[winRow, 0] == gameGrid[winRow, 1] && gameGrid[winRow, 1] == gameGrid[winRow, 2])
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// Calculates winnings depending on stake put down
        /// </summary>
        /// <param name="coins">ammount of coins put down for stake</param>
        /// <param name="stakeWinning">ammount of the stake put down</param>
        /// <returns>returns total amount of coins</returns>
        static int calcWinnings (int coins, checkStake stakeWinning)
        {
                if (stakeWinning == checkStake.oneCoin)
                {
                    int total = coins * 1;
                    return total;
                }
                if (stakeWinning == checkStake.twoCoins)
                {
                    int total = coins * 2;
                    return total;
                }
                if (stakeWinning == checkStake.threeCoins)
                {
                    int total = coins * 3;
                    return total;
                }
                else
                    return 0;
            }
        }

    }

