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
            string play = "";

            ///Displays welcome message with rules of the game and displays opening ammount of coins
            ///User to press any key to enter game or x to exit game
            welcome(stack);
            Console.WriteLine($"Press any Key play press [x to quit]");
            play = Console.ReadLine();
            if (play == "x")
                return;
            else

                Console.Clear();
            ///Main for Loop of game if there are coins in the stack gives the user an option to enter 1,2 or 3 coins
            ///goes through program works out if there was a win or not tells user if they have won or not and returns to 
            ///the beggining of the loop 
            ///if coins reach zero then exit the program 
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
                ///Works out winning lines from grid 
                bool topLine = checkHorizontal(grid, 0);
                bool midLine = checkHorizontal(grid, 1);
                bool botLine = checkHorizontal(grid, 2);
                bool diagLefttoRight = DiagCheck(grid, checkDiag.leftToRight);
                bool diagRighttoLeft = DiagCheck(grid, checkDiag.RightToLeft);

                ///Tells program what to do when each stake (1,2 or 3) is placed)
                if (inputStake == 1)
                {
                    stack = totalCoins(stack, checkStake.oneCoin);
                    if (midLine == true)
                    {
                        stack = calcWinnings(inputStake, stack, checkStake.oneCoin);
                    }

                }
                if (inputStake == 2)
                {
                    stack = totalCoins(stack, checkStake.twoCoins);
                    if (topLine || botLine || midLine == true)
                    {
                        stack = calcWinnings(inputStake, stack, checkStake.twoCoins);
                    }

                }
                if (inputStake == 3)
                {
                    stack = totalCoins(stack, checkStake.threeCoins);
                    if (topLine || botLine || midLine || diagLefttoRight || diagRighttoLeft == true)
                    {
                        stack = calcWinnings(inputStake, stack, checkStake.threeCoins);
                    }
                }
                //Displays win or lose depending on if there were any winning lines or not
                displayResult(topLine, botLine, midLine, diagLefttoRight, diagRighttoLeft);
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
    
        /// <summary>
        /// Checks to see if stake has won and returns True if user has won or false if user has not won
        /// </summary>
        /// <param name="gameGrid">Grid of numbers (grid)</param>
        /// <param name="winRow">Which row has all the same numbers</param>
        /// <returns>True or False</returns>
        static bool checkHorizontal(int[,] gameGrid, int winRow )
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
        /// Check if Diagnal lines are winning lines, using righttoleft and lefttoright enum 
        /// </summary>
        /// <param name="gameGrid">The Game Grid</param>
        /// <param name="DiagLine">Check left to Right and Right to Left diag lines</param>
        /// <returns>True or False if Lines are winning lines or not </returns>
        static bool DiagCheck(int[,] gameGrid, checkDiag DiagLine)
        {
            if (DiagLine == checkDiag.leftToRight)
            {
                if (gameGrid[0, 0] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 2])
                    return true;
                else
                {
                    return false;
                }
            }
                if (DiagLine == checkDiag.RightToLeft)
                {
                    if (gameGrid[0, 2] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 0])

                        return true;
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            /// <summary>
            /// Displays win or lose message 
            /// </summary>
            /// <param name="top">top (topLine) line of the grid</param>
            /// <param name="middle">Middle line (midLine) of the grid</param>
            /// <param name="bottom">Bottom line of the grid (botLine)</param>
            static void displayResult (bool top, bool middle, bool bottom, bool DiagLtoR, bool DiagRtoL)
        {
            if (top || middle || bottom || DiagLtoR || DiagRtoL == true)
            {
                Console.WriteLine($"You Won =)");
            }
            else
            {
                Console.WriteLine($"You lose, spin again");
            }
        }
        /// <summary>
        /// Calculates winnings depending on stake put down
        /// </summary>
        /// <param name="coins">ammount of coins put down for stake</param>
        /// <param name="stakeWinning">ammount of the stake put down</param>
        /// <returns>returns total amount of coins</returns>
        static int calcWinnings (int coins, int pot, checkStake stakeWinning)
        {
                if (stakeWinning == checkStake.oneCoin)
                {
                    int total = coins + 1 + pot;
                    return total;
                }
                if (stakeWinning == checkStake.twoCoins)
                {
                    int total = coins * 2 + pot;
                    return total;
                }
                if (stakeWinning == checkStake.threeCoins)
                {
                    int total = coins * 3 + pot;
                    return total;
                }
                else
                    return 0;
            }
        }


    }
     ///TODO: Clean up exit of game display total winning coins when program exitedby player 