using System;

namespace SlotMachine3
{
    class Program
    {
        public enum checkDiag
        {
            Left,
            Right,
        }
        public enum checkStake
        {
            ThreeCoins,
            TwoCoins,
            OneCoin,
        }
        static void Main(string[] args)
        {

            int row;
            int col;
            int[,] grid = new int[3, 3];
            var ranNum = new Random();
            int stack = 20;
            string play = "";
            int inputStake = 0;


         

            ///Displays welcome message with rules of the game and displays opening ammount of coins
            ///User to press any key to enter game or x to exit game
            welcome(stack);
            Console.WriteLine($"Press any Key play press [x to quit]");
            play = Console.ReadLine();
            if (play == "x")
            {
                Console.WriteLine($"Game over");
                return;
            }
            else
                Console.Clear();

            ///Main for Loop of game, if there are coins in the stack gives the user an option to enter 1,2 or 3 coins
            ///goes through program works out if there was a win or not tells user if they have won or not and returns to 
            ///the beggining of the loop 
            ///if coins reach zero then exit the program 
            for (stack = 20; stack > 0;)
            {
                CoinDisplay(stack);
                ///Enter stake and uses totalCoins function to remove coins from coin stack
                Console.WriteLine($"Enter stake you would like to play (1,2 or 3 coins)");
                //Checks input from user is correct data type and displays message if incorrect
                while (!int.TryParse(Console.ReadLine(), out inputStake))
                {
                    Console.WriteLine($"You entered an invalid character");
                    Console.WriteLine($"Please enter the amount of coins you would like to gamble (Maximum of 3 coins)");
                }
                Console.Clear();
                if (inputStake > 3)
                {
                    Console.WriteLine($"Maximum bet is 3 coins please enter an number between 1 and 3");
                    while (!int.TryParse(Console.ReadLine(), out inputStake))
                    {
                        Console.WriteLine($"You entered an invalid character");
                        Console.WriteLine($"Please enter the amount of coins you would like to gamble (Maximum of 3 coins)");
                    }
                }
                else
                    ///Populates grid with random numbers from 0 - 2 
                    for (row = 0; row < 3; row++)
                    {
                        for (col = 0; col < 3; col++)
                        {
                            grid[row, col] = ranNum.Next(0, 3);
                            //  Console.Write($" {grid[row, col]} ");
                            //if (col == 2)
                            //  Console.WriteLine();
                            InputGridNumbers(row, col, grid);
                        }
                    }
                ///Works out winning lines from grid 
                bool topLine = CheckHorizontal(grid, 0);
                bool midLine = CheckHorizontal(grid, 1);
                bool botLine = CheckHorizontal(grid, 2);
                bool diagLefttoRight = DiagCheck(grid, checkDiag.Left);
                bool diagRighttoLeft = DiagCheck(grid, checkDiag.Right);

                ///Tells program what to do when each stake (1,2 or 3) is placed)
                if (inputStake == 1)
                {
                    stack = TotalCoins(stack, checkStake.OneCoin);
                    if (midLine == true)
                    {
                        stack = CalcWinnings(inputStake, stack, checkStake.OneCoin);
                    }
                    DisplayResult(topLine, midLine, botLine, diagLefttoRight, diagRighttoLeft, checkStake.OneCoin);
                }
                if (inputStake == 2)
                {
                    stack = TotalCoins(stack, checkStake.TwoCoins);
                    if (topLine || botLine || midLine == true)
                    {
                        stack = CalcWinnings(inputStake, stack, checkStake.TwoCoins);
                    }
                    DisplayResult(topLine, midLine, botLine, diagLefttoRight, diagRighttoLeft, checkStake.TwoCoins);
                }
                if (inputStake == 3)
                {
                    stack = TotalCoins(stack, checkStake.ThreeCoins);
                    if (topLine || botLine || midLine || diagLefttoRight || diagRighttoLeft == true)
                    {
                        stack = CalcWinnings(inputStake, stack, checkStake.ThreeCoins);
                    }
                    DisplayResult(topLine, midLine, botLine, diagLefttoRight, diagRighttoLeft, checkStake.ThreeCoins);
                }
                if (stack == 0)
                {
                    Console.Clear();
                    GameOverDisplay(stack);
                }
                Console.WriteLine($"Press any key to spin again press [x] to quit and collect you coins");
                string SpinAgain = Console.ReadLine();
                if (SpinAgain == "x")
                {
                    Console.Clear();
                    GameOverDisplay(stack);
                    return;
                }
            }

        }


        /// <summary>
        /// Function to dispay ammount of coins users has 
        /// </summary>
        /// <param name="coins">Ammount of coins user has left in stack</param>
        static void CoinDisplay(int coins)
        {
            Console.WriteLine($"*********************");
            Console.WriteLine($"You have {coins} coins left");
            Console.WriteLine($"*********************");
        }
        static void GameOverDisplay(int coins)
        {
            Console.WriteLine($"**********************");
            Console.WriteLine($"******Game over******");
            Console.WriteLine($"You have {coins} coins left");
            Console.WriteLine($"**********************");
        }
        /// <summary>
        /// Function for Welcome display shows users the rules of the game
        /// </summary>
        static void WelcomDisplay()
        {
            Console.WriteLine($"Enter how many coins you want to gamble (max of 3 coins)");
            Console.WriteLine($"If you gamble 3 coins you win on ALL Horizontal lins and Diagnal lins");
            Console.WriteLine($"If you gamble 2 coins you can win on ALL Hozizontal lines");
            Console.WriteLine($"If you gamble 1 coin you can win on just the middle Hozizontal line");
        }
        static void welcome(int coins)
        {
            WelcomDisplay();
            CoinDisplay(coins);
        }
        /// <summary>
        /// Inputs Random numbers in to grid format
        /// </summary>
        /// <param name="gridRow">row</param>
        /// <param name="gridCol">col</param>
        /// <param name="gameGrid">grid</param>
        static void InputGridNumbers(int gridRow, int gridCol, int[,] gameGrid)
        {
            Console.Write($" {gameGrid[gridRow, gridCol]} ");
            if (gridCol == 2)
            {
                Console.WriteLine();
            }
        }
     
        /// <summary>
        /// This works out stake and how much to take from coin stack depending on which stake is placed
        /// </summary>
        /// <param name="coins">Total ammount of coins</param>
        /// <param name="checking">which amount of coins to take    `away from coin stack (1,2 or 3)</param>
        /// <returns>Total number of coins left after stake has been placed</returns>
        static int TotalCoins(int coins, checkStake checking)
        {
            if (checking == checkStake.OneCoin)
            {
                int result = coins - 1;
                return result;
            }
            if (checking == checkStake.TwoCoins)
            {
                int result = coins - 2;
                return result;
            }
            if (checking == checkStake.ThreeCoins)
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
        static bool CheckHorizontal(int[,] gameGrid, int winRow)
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
        static bool DiagCheck(int[,] gameGrid, checkDiag diagLine)
        {
            if (diagLine == checkDiag.Left)
            {
                if (gameGrid[0, 0] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 2])
                    return true;
                else
                {
                    return false;
                }
            }
            if (diagLine == checkDiag.Right)
            {
                if (gameGrid[0, 2] == gameGrid[1, 1] && gameGrid[1, 1] == gameGrid[2, 0])
                    return true;
                else
                    return false;

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
        static void DisplayResult(bool top, bool middle, bool bottom, bool diagLtoR, bool diagRtoL, checkStake BetPlaced)
        {
            if (BetPlaced == checkStake.ThreeCoins)
            {

                if (top || middle || bottom || diagLtoR || diagRtoL == true)
                {
                    Console.WriteLine($"You Won =)");
                }
                else
                {
                    Console.WriteLine($"You lose, spin again");
                }
            }
            if (BetPlaced == checkStake.TwoCoins)
            {
                if (top || middle || bottom == true)
                {
                    Console.WriteLine($"You Won =)");
                }
                else
                {
                    Console.WriteLine($"You lose, spin again");
                }
            }
            if (BetPlaced == checkStake.OneCoin)
            {
                if (middle == true)
                {
                    Console.WriteLine($"You Won =)");
                }
                else
                {
                    Console.WriteLine($"You lose, spin again");
                }
            }
        }
        /// <summary>
        /// Calculates winnings depending on stake put down
        /// </summary>
        /// <param name="coins">ammount of coins put down for stake</param>
        /// <param name="stakeWinning">ammount of the stake put down</param>
        /// <returns>returns total amount of coins</returns>
        static int CalcWinnings(int coins, int pot, checkStake StakeWinning)
        {
            checkStake SelectedStake = (StakeWinning);

            switch (StakeWinning)
            {
                case checkStake.OneCoin:
                    int total = coins + 1 + pot;
                    return total;
                case checkStake.TwoCoins:
                    int total1 = coins * 2 + pot;
                    return total1;
                case checkStake.ThreeCoins:
                    int total2 = coins * 3 + pot;
                    return total2;
                default:
                    return 0;
                
            }
            
        }
    }
}
///TODO: Clean up exit of game display total winning coins when program exitedby player 
