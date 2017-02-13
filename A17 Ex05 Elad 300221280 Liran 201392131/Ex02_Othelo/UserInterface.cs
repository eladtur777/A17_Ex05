//using System;
//using System.Text;


//namespace Ex02_Othelo
//{
//    public class UserInterface
//    {
//        private static int m_GetUserBoardSize = 0;
//        private static int m_GetUserGameType = 0;
//        private GameModel m_Game;
//        private string m_FirstPlayerName;
//        private bool m_LegalMoveForFirstPlayer = true;
//        private bool m_LegalMoveForSecondPlayer = true;

//        public void CreateNewGame()
//        {
//            StringBuilder firstLineForuser = new StringBuilder();
//            firstLineForuser.Append("Welcome To Othelo Game :");
//            firstLineForuser.Append(Environment.NewLine);
//            firstLineForuser.Append("========================");
//            firstLineForuser.Append(Environment.NewLine);
//            Console.WriteLine(firstLineForuser);
//            Console.Write("Please enter your name (First Player): ");
//            m_FirstPlayerName = Console.ReadLine();
//            Console.WriteLine(string.Format("{0} please choose Game mode:{1}1. Player VS Player{1}2. Player VS Computer", m_FirstPlayerName, Environment.NewLine));
//            string userChoiceOfGame = Console.ReadLine();
//            bool userCoiceToParse = int.TryParse(userChoiceOfGame, out m_GetUserGameType);
//            bool legal = false;
//            bool firstMenuWasChossen = false;
//            do
//            {
//                if ((m_GetUserGameType == (int)EnumGameMenu.e_Menu.PlayerVsPlayer) && userCoiceToParse)
//                {
//                    legal = true;
//                    firstMenuWasChossen = true;
//                    break;
//                }
//                else if ((m_GetUserGameType == (int)EnumGameMenu.e_Menu.PlayerVsComputer) && userCoiceToParse)
//                {
//                    legal = true;
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("Please enter valid choice , 1 OR 2 only : ");
//                    userChoiceOfGame = Console.ReadLine();
//                    userCoiceToParse = int.TryParse(userChoiceOfGame, out m_GetUserGameType);
//                }
//            }
//            while (!legal);

//            if (firstMenuWasChossen)
//            {
//                PlayerVsPlayerBoard(m_FirstPlayerName);
//            }
//            else
//            {
//                PlayerVsComputerBoard(m_FirstPlayerName);
//            }
//        }

//        // $G$ CSS-999 (-3) private method name shouldn't start with capital letter.
//        private void PlayerVsPlayerBoard(string i_FirstPlayerName)
//        {
//            Console.WriteLine("Please enter second player name : ");
//            string secondPlayerName = Console.ReadLine();
//            Console.WriteLine("please enter board size (6 for 6x6 board) OR (8 for 8x8 board): ");
//            string getUserChoise = Console.ReadLine();
//            CheckLegalBoardSizeFromPlayer(getUserChoise);
//            m_Game = new GameModel(m_GetUserBoardSize, i_FirstPlayerName, secondPlayerName);
//            Console.WriteLine(m_Game.BoardGameCreator);
//            ////ManipulateUserchoice
//            ManipulateUserChoiceForFirstPlayer();
//        }

//        private void PlayerVsComputerBoard(string i_PlayerName)
//        {
//            Console.WriteLine("please enter board size (6 for 6x6 board) OR (8 for 8x8 board): ");
//            string getUserChoise = Console.ReadLine();
//            CheckLegalBoardSizeFromPlayer(getUserChoise);
//            m_Game = new GameModel(m_GetUserBoardSize, i_PlayerName, "Computer");
//            Console.WriteLine(m_Game.BoardGameCreator);
//            ////ManipulateUserchoice
//            ManipulateUserChoiceForFirstPlayer();
//        }

//        private void ManipulateUserChoiceForFirstPlayer()
//        {
//            ////first check if there is legal mooves for the player in all board
//            if (!m_Game.ThereIsExisitingLegalMove(m_Game.FirstPlayer))
//            {
//                ////if no, then pick a flag and call to ManipulateUserChoiceForO
//                m_LegalMoveForFirstPlayer = false;
//                Console.WriteLine(string.Format("{0} you out of moves", m_Game.FirstPlayer.PlayerName));
//                ManipulateUserChoiceForSecondPlayer();
//            }
//            else
//            {
//                ////if yes ,dont pick up a flag and do this:
//                m_LegalMoveForFirstPlayer = true;
//                Console.Write(string.Format("{0} is your turn, your sign is (X){1}Please enter point in range or Q for exit game:", m_Game.FirstPlayer.PlayerName, Environment.NewLine));
//                string getPlayerCell = Console.ReadLine();
//                ////check legal input && Check Legal Neighbours
//                CheckLegalInputCell(getPlayerCell, m_Game.FirstPlayer);
//                ////clear screen
//                //Screen.Clear();
//                ////print update board
//                Console.WriteLine(m_Game.BoardGameCreator);
//                Console.WriteLine(string.Format("{0} score: {1}", m_Game.FirstPlayer.PlayerName, m_Game.FirstPlayer.PlayerScore));
//                Console.WriteLine(string.Format("{0} score: {1}", m_Game.SecondPlayer.PlayerName, m_Game.SecondPlayer.PlayerScore));
//                ////player 2 turn
//                ManipulateUserChoiceForSecondPlayer();
//            }
//        }

//        public void ManipulateUserChoiceForSecondPlayer()
//        {
//            if (m_GetUserGameType == (int)EnumGameMenu.e_Menu.PlayerVsComputer)
//            {
//                ////computer turn
//                //// first check if there is legal mooves for computer in all board
//                if (m_Game.ThereIsExisitingLegalMove(m_Game.SecondPlayer))
//                {
//                    m_LegalMoveForSecondPlayer = true;
//                    m_Game.LegalMove(m_Game.SecondPlayer);
//                    ////clear screen
//                   // Screen.Clear();
//                    ////print scores and updated board
//                    Console.WriteLine(m_Game.BoardGameCreator);
//                    Console.WriteLine(string.Format("{0} score: {1}", m_Game.FirstPlayer.PlayerName, m_Game.FirstPlayer.PlayerScore));
//                    Console.WriteLine(string.Format("{0} score: {1}", m_Game.SecondPlayer.PlayerName, m_Game.SecondPlayer.PlayerScore));
//                    ////player 2 turn
//                    ManipulateUserChoiceForFirstPlayer();
//                }
//                else
//                {
//                    m_LegalMoveForSecondPlayer = false;
//                    if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
//                    {
//                        GameOver();
//                    }
//                    else if (!m_LegalMoveForSecondPlayer)
//                    {
//                        Console.WriteLine("Computer out of moves");
//                        ManipulateUserChoiceForFirstPlayer();
//                    }
//                }
//            }
//            else
//            {
//                if (m_Game.ThereIsExisitingLegalMove(m_Game.SecondPlayer))
//                {
//                    ////if yes ,dont pick up a flag and do this:
//                    m_LegalMoveForSecondPlayer = true;
//                    Console.Write(string.Format("{0} is your turn, your sign is (O){1}Please enter point in range or Q for exit game:", m_Game.SecondPlayer.PlayerName, Environment.NewLine));
//                    string getPlayerCell = Console.ReadLine();
//                    CheckLegalInputCell(getPlayerCell, m_Game.SecondPlayer);
//                    ////clear screen
//                  //  Screen.Clear();
//                    ////Print update board
//                    Console.WriteLine(m_Game.BoardGameCreator);
//                    Console.WriteLine(string.Format("{0} score: {1}", m_Game.FirstPlayer.PlayerName, m_Game.FirstPlayer.PlayerScore));
//                    Console.WriteLine(string.Format("{0} score: {1}", m_Game.SecondPlayer.PlayerName, m_Game.SecondPlayer.PlayerScore));
//                    ////player 2 turn
//                    ManipulateUserChoiceForFirstPlayer();
//                }
//                else
//                {
//                    m_LegalMoveForSecondPlayer = false;
//                    if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
//                    {
//                        GameOver();
//                    }
//                    else if (!m_LegalMoveForSecondPlayer)
//                    {
//                        Console.WriteLine(string.Format("{0} you out of moves", m_Game.SecondPlayer.PlayerName));
//                        ManipulateUserChoiceForFirstPlayer();
//                    }
//                }
//            }
//        }

//        private void CheckLegalBoardSizeFromPlayer(string i_GetPlayerBoardSize)
//        {
//            bool parseUserInput = int.TryParse(i_GetPlayerBoardSize, out m_GetUserBoardSize);
//            if (((m_GetUserBoardSize == (int)EnumBoardSize.e_Size.Eight - 2 || m_GetUserBoardSize == (int)EnumBoardSize.e_Size.Ten - 2) && parseUserInput) == false)
//            {
//                do
//                {
//                    Console.WriteLine("please enter valid board size of 6 OR 8");
//                    i_GetPlayerBoardSize = Console.ReadLine();
//                    parseUserInput = int.TryParse(i_GetPlayerBoardSize, out m_GetUserBoardSize);
//                }
//                while (((m_GetUserBoardSize == (int)EnumBoardSize.e_Size.Eight - 2 || m_GetUserBoardSize == (int)EnumBoardSize.e_Size.Ten - 2) && parseUserInput) == false);
//            }
//        }

//        private void CheckLegalInputCell(string i_GetPlayerCell, Player i_player)
//        {
//            char[] playerInput;
//            playerInput = i_GetPlayerCell.ToCharArray();
//            bool firstBreakFlag = false;
//            do
//            {
//                if ((playerInput.Length == 1) && (playerInput[0] == 'Q'))
//                {
//                    // $G$ NTT-999 (-10) Should use Environment.NewLine rather than \n.
//                    Console.WriteLine(string.Format("{0}{1}", "\n", "Bye Bye...."));
//                    // $G$ DSN-999 (-1) You should avoid brutally terminating the process. This should be a part of the main game flow.
//                    Environment.Exit(0);
//                }

//                if (m_Game.Board.Boardsize.Equals(8))
//                {
//                    if ((playerInput.Length == 2) && (playerInput[0] >= 'A' && playerInput[0] <= 'F') &&
//                          (playerInput[1] >= '1' && playerInput[1] <= '6'))
//                    {
//                        firstBreakFlag = true;
//                        break;
//                    }
//                }

//                if (m_Game.Board.Boardsize.Equals(10))
//                {
//                    if ((playerInput.Length == 2) && (playerInput[0] >= 'A' && playerInput[0] <= 'H') &&
//                     (playerInput[1] >= '1' && playerInput[1] <= '8'))
//                    {
//                        firstBreakFlag = true;
//                        break;
//                    }
//                }

//                if (!firstBreakFlag)
//                {
//                    Console.Write("Wrong input!!! please enter legal coordinates (for example: A1,B2..):");
//                    i_GetPlayerCell = Console.ReadLine();
//                    Array.Clear(playerInput, 0, playerInput.Length);
//                    playerInput = i_GetPlayerCell.ToCharArray();
//                }
//            }
//            while (!firstBreakFlag);
//            ////  return Point;
//            UpdateBoard(playerInput, i_player);
//        }

//        public int SwitchConvert(char i_GetChar)
//        {
//            // $G$ DSN-999 (-1) Really? you couldn't write a generic convertor instead? :(
//            switch (i_GetChar)
//            {
//                case 'A':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.A;
//                case 'B':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.B;
//                case 'C':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.C;
//                case 'D':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.D;
//                case 'E':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.E;
//                case 'F':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.F;
//                case 'G':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.G;
//                case 'H':
//                    return (int)EnumLettersToNumbers.e_ConvertToNumbers.H;
//                default: return 0;
//            }
//        }

//        private void UpdateBoard(char[] i_charArray, Player i_player)
//        {
//            EnumLettersToNumbers m_ConvertPlayerLetter = new EnumLettersToNumbers();
//            bool isLegalMove = false;
//            Point pointToSend = new Point(i_charArray[1] - '0', SwitchConvert(i_charArray[0]));
//            isLegalMove = m_Game.LegalMove(i_player, pointToSend, EnumCheckOrChange.e_OnlyCheck.No);
//            if (!isLegalMove)
//            {
//                Console.Write("Ilegal move, please insert legal move :");
//                string GetPlayerCell = Console.ReadLine();
//                CheckLegalInputCell(GetPlayerCell, i_player);
//            }
//        }

//        private void GameOver()
//        {
//            CallTheWinner(m_Game.GetFirstPlayersScore, m_Game.GetSecondPlayersScore);
//            Console.WriteLine("Do you wish for another game? Y for yes or Q for exit:");
//            string anotherGame = Console.ReadLine();
//            bool inCorrectInput = true;
//            do
//            {
//                if (anotherGame.Equals("Y"))
//                {
//                    m_LegalMoveForFirstPlayer = true;
//                    m_LegalMoveForSecondPlayer = true;
//                    if (m_GetUserGameType == (int)EnumGameMenu.e_Menu.PlayerVsComputer)
//                    {
//                        m_Game = new GameModel(m_Game.Board.Boardsize - 2, m_Game.FirstPlayer.PlayerName, "Computer");
//                      //  Screen.Clear();
//                        Console.WriteLine(m_Game.BoardGameCreator);
//                        inCorrectInput = false;
//                        ManipulateUserChoiceForFirstPlayer();
//                    }
//                    else
//                    {
//                        inCorrectInput = false;
//                        m_Game = new GameModel(m_Game.Board.Boardsize - 2, m_Game.FirstPlayer.PlayerName, m_Game.SecondPlayer.PlayerName);
//                     //   Screen.Clear();
//                        Console.WriteLine(m_Game.BoardGameCreator);
//                        ManipulateUserChoiceForFirstPlayer();
//                    }
//                }
//                else if (anotherGame.Equals("Q"))
//                {
//                    Console.WriteLine(string.Format("Bye Bye...."));
//                    inCorrectInput = false;
//                    Environment.Exit(0);
//                }
//                else
//                {
//                    Console.WriteLine("Wrong input!!! please enter one from the following option: Y or Q:");
//                    anotherGame = Console.ReadLine();
//                }
//            }
//            while (inCorrectInput);
//        }

//        private void CallTheWinner(int i_FirstPlayerScore, int i_SecondPlayerScore)
//        {
//            StringBuilder callTheWinner = new StringBuilder();
//            if (i_FirstPlayerScore > i_SecondPlayerScore)
//            {
//                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, Environment.NewLine));
//            }

//            if (i_FirstPlayerScore < i_SecondPlayerScore)
//            {
//                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
//            }

//            if (i_FirstPlayerScore == i_SecondPlayerScore)
//            {
//                callTheWinner.Append(string.Format("Game Over, Draw!!!{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
//            }

//            Console.WriteLine(callTheWinner);
//        }
//    }
//}
