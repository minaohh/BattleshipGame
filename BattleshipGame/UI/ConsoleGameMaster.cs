using System;

namespace Battleship
{
    public class GameMasterConsole
    {
        #region Members

        private static string MSG_INVALID_INPUT = "Invalid input! Please choose a letter from one of the choices.";
        private BattleshipBoard _battleship;
        private bool _won = false;

        #endregion

        #region Constructors

        public GameMasterConsole()
        {
            _battleship = new BattleshipBoard();
        }

        public GameMasterConsole(BattleshipBoard board)
        {
            _battleship = board;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Play the 'Battleship' Game via console
        /// </summary>
        public void RunGame()
        {
            try
            {
                NewGame();
                char ans = '\0';

                do
                {
                    ShowGameOptions();

                    string consoleInput = Console.ReadLine().Trim().ToLower();
                    bool validInput = Utils.ValidGameOptionInput(consoleInput);
                    Console.WriteLine();

                    if (!validInput)
                    {
                        Console.WriteLine(MSG_INVALID_INPUT);
                        continue;
                    }

                    ans = Convert.ToChar(consoleInput);

                    switch (ans)
                    {
                        case Constants.GAME_OPTION_NEW_GAME:
                            NewGame();
                            break;

                        case Constants.GAME_OPTION_SETUP_MODE:
                            if (!_battleship.IsSetupComplete)
                            {
                                SetupShips();
                            }
                            else
                            {
                                Console.WriteLine("Setup already completed. Please enter attack mode to start the game.");
                            }
                            break;

                        case Constants.GAME_OPTION_ATTACK_MODE:
                            if (_battleship.IsSetupComplete)
                            {
                                AttackMode();
                            }
                            else
                            {
                                Console.WriteLine("There are no ships on the board. Please setup the ships before playing.");
                            }

                            break;

                        case Constants.GAME_OPTION_EXIT:
                            ExitGame();
                            break;

                        default:
                            Console.WriteLine(MSG_INVALID_INPUT);
                            break;
                    }


                } while (ans != Constants.GAME_OPTION_EXIT);

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong. Please try again. Details:\n\t{0}", ex);
            }
        }

        /// <summary>
        /// Print the battleship board
        /// </summary>
        /// <param name="showShips">Show battleships on the board, default is false</param>
        public void PrintBoard(bool showShips = false)
        {
            Console.WriteLine("     A   B   C   D   E   F   G   H   I   J  ");

            for (int i = 0; i < _battleship.Board.GetLength(0); i++)
            {
                string spaces = i == _battleship.Board.GetLength(0) - 1 ? " " : "  ";
                Console.Write("{0}{1}", i + 1, spaces);

                for (int j = 0; j < _battleship.Board.GetLength(1); j++)
                {
                    string val = "-";

                    if (_battleship.Board[i, j] == Constants.SHIP_MARK && showShips)
                    {
                        val = Constants.SHIP_MARK_PRINT;
                    }
                    else if (_battleship.Board[i, j] == Constants.MISS_MARK)
                    {
                        val = Constants.MISS_MARK_PRINT;
                    }
                    else if (_battleship.Board[i, j] == Constants.HIT_MARK)
                    {
                        val = Constants.HIT_MARK_PRINT;
                    }

                    Console.Write("| {0} ", val);
                }

                Console.WriteLine("|");
            }
        }

        #endregion

        #region Game Options

        private void ExitGame()
        {
            Console.WriteLine("Exiting game... Here's the final board");

            PrintBoard(true);

            Console.WriteLine("Thank you for playing! :)\nPress any key to stop the program...");
        }

        private void AttackMode()
        {
            Console.WriteLine("\n--------\nYou're now in ATTACK MODE\n--------\n", Constants.ATTACK_MODE_EXIT);

            int count = 1;
            bool exit = false;

            do
            {
                Console.Write("\n[Shot {0}] Enter shot location (e.g. A7): ", count);
                string input = Console.ReadLine().Trim().ToLower();
                Console.WriteLine();

                //Validate input
                if (!Utils.ValidAttackInput(input, out char colInput, out int rowInput))
                {
                    Console.WriteLine("Invalid attack input! Please a letter from A to J, then a number from 1 to 10.");
                    continue;
                }

                Console.WriteLine("Attacking coordinates '{0}-{1}'...", colInput, rowInput);

                // Add the attack
                bool hit = _battleship.Attack(colInput, rowInput);

                Console.WriteLine(hit ? "> HIT!" : "> MISS");
                PrintBoard();

                // Check if all ships are hit
                if (_battleship.AllShipsSunk())
                {
                    ShowWinMessage();
                    Console.WriteLine("It took you {0} shots to win the game.", count);
                    Console.WriteLine("Now taking you back to options pane...");

                    _won = true;
                    exit = true;
                }

                count++;
            } while (!exit);
        }

        private void SetupShips()
        {
            AddBattleship();
        }

        private void AddBattleship()
        {
            Console.WriteLine("Adding a battleship on the board...");

            _battleship.AddShips();

            Console.WriteLine("A battleship is added on the board.");
        }

        private void NewGame()
        {
            ShowWelcomeMessage();
            _battleship.NewGame();
            _won = false;
        }

        #endregion

        #region Console Text / Info

        private void ShowWinMessage()
        {
            Console.WriteLine("\n");
            Console.WriteLine(@"\ \    / /|_ _|| \| |");
            Console.WriteLine(@" \ \/\/ /  | | | .` |");
            Console.WriteLine(@"  \_/\_/  |___||_|\_|");
            Console.WriteLine("Congratulations! The ship has sunk.");
        }

        private void ShowGameOptions()
        {
            Console.WriteLine("\n");
            Console.WriteLine("__| |______________________________________| |__ ");
            Console.WriteLine("(__   ______________________________________   __)");
            Console.WriteLine("   | |                                      | |   ");
            Console.WriteLine("   | | Game Options:                        | |   ");

            if (!_won)
            {
                Console.WriteLine("   | |    {0}. Setup ships                    | |   ", Constants.GAME_OPTION_SETUP_MODE);
                Console.WriteLine("   | |    {0}. Enter 'Attack' mode            | |   ", Constants.GAME_OPTION_ATTACK_MODE);
            }
            
            Console.WriteLine("   | |    {0}. New Game (create new board)    | |   ", Constants.GAME_OPTION_NEW_GAME);
            Console.WriteLine("   | |    {0}. Exit Game                      | |   ", Constants.GAME_OPTION_EXIT);
            Console.WriteLine(" __| |______________________________________| |__ ");
            Console.WriteLine("(__   ______________________________________   __)");
            Console.WriteLine("   | |                                      | |   ");
            Console.Write("Answer:  ");
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine(".-=~=-.                                   .-=~=-.");
            Console.WriteLine("(__  _)-._.-=-._.-=-._.-=-._.-=-._.-=-._.-(__  _)");
            Console.WriteLine("( _ __)                                   ( _ __)");
            Console.WriteLine("(__  _)   Welcome to                      (__  _)");
            Console.WriteLine("(_ ___)          BATTLESHIP               (_ ___)");
            Console.WriteLine("(__  _)                    Game           (__  _)");
            Console.WriteLine("( _ __)                                   ( _ __)");
            Console.WriteLine("(__  _)      (Single Player Mode)         (__  _)");
            Console.WriteLine("(__  _)                                   (__  _)");
            Console.WriteLine("(_ ___)-._.-=-._.-=-._.-=-._.-=-._.-=-._.-(_ ___)");
            Console.WriteLine("`-._.-'                                   `-._.-'");

            Console.WriteLine("The game board will be printed after every attack.\nLegend:");
            Console.WriteLine("{0} - ship", Constants.SHIP_MARK_PRINT);
            Console.WriteLine("{0} - hit" , Constants.HIT_MARK_PRINT);
            Console.WriteLine("{0} - miss", Constants.MISS_MARK_PRINT);

            Console.WriteLine("Note: all options and commands are case insensitive.");
        }

        #endregion
    }
}
