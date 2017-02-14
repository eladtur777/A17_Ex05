using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex02_Othelo;

namespace Ex05.OtheloUI
{
    public partial class Game : Form
    {
        const int k_PictureBoxArea = 60;
        const int k_SpaceBetweenPictures = 4;
        private int m_BoardSize;
        private PictureBox[,] pictureBoxarray;
        private GameModel m_GameModel;
        private bool player1Turn = true;
        private bool player2Turn = false;
        private bool m_LegalMoveForFirstPlayer = true;
        private bool m_LegalMoveForSecondPlayer = true;
        System.Drawing.Point pictureLocation;

        public Game()
        {
            m_GameModel = new GameModel(GameController.BoardSize, GameController.FirstPlayerName, GameController.SecondPlayerName);
            m_BoardSize = m_GameModel.Board.Boardsize;
            pictureBoxarray = new PictureBox[m_BoardSize, m_BoardSize];
            pictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;

            switch (GameController.BoardSize)
            {
                case (int)eBoardSize.Six:
                    this.ClientSize = new System.Drawing.Size(390, 390);
                    break;
                case (int)eBoardSize.Eight:
                    this.ClientSize = new System.Drawing.Size(516, 516);
                    break;
                case (int)eBoardSize.Ten:
                    this.ClientSize = new System.Drawing.Size(645, 645);
                    break;
                case (int)eBoardSize.Twelve:
                    this.ClientSize = new System.Drawing.Size(774, 774);
                    break;
            }

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }



        private void Game_Load(object sender, EventArgs e)
        {

          //  System.Drawing.Point pictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);

            for (int i = 1; i < m_BoardSize - 1; i++)
            {
                for (int j = 1; j < m_BoardSize - 1; j++)
                {
                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)(eGameSigns.X))
                    {

                        pictureBoxarray[i, j] = new PictureBox();
                        pictureBoxarray[i, j].Image = Properties.Resources.CoinRed;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        //    pictureBoxarray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.Controls.Add(pictureBoxarray[i, j]);

                    }

                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.O)
                    {
                        pictureBoxarray[i, j] = new PictureBox();
                        pictureBoxarray[i, j].Image = Properties.Resources.CoinYellow;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        //  pictureBoxarray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.Controls.Add(pictureBoxarray[i, j]);


                    }

                    else if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.None)
                    {

                        pictureBoxarray[i, j] = new PictureBox() { BackColor = Color.CadetBlue };
                        pictureBoxarray[i, j].Image = Properties.Resources.White;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureBoxarray[i, j].Name = string.Format("{0}{1}",i,j);
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        pictureBoxarray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        pictureBoxarray[i, j].MouseHover += new EventHandler(this.pictureBox_MouseHover);
                        pictureBoxarray[i, j].MouseLeave += new EventHandler(this.pictureBox_MouseLeave);
                        this.Controls.Add(pictureBoxarray[i, j]);
                    }

                }

                pictureLocation.X = k_SpaceBetweenPictures;
                pictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;

            }

        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.CadetBlue;
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.BackColor = Color.Coral;
        }

        private void pictureBox_Mouseclick(object sender, EventArgs e)
        {
            if (player1Turn == true)
            {
                ////first check if there is legal mooves for the player in all board
                if (!m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer))
                {
                    ////if no, then pick a flag and call to ManipulateUserChoiceForO
                    m_LegalMoveForFirstPlayer = false;
                    MessageBox.Show(string.Format("{0} you out of moves", m_GameModel.FirstPlayer.PlayerName));
                    manipulateUserChoiceForSecondPlayer();
                }
                else
                {
                    ////if yes ,dont pick up a flag and do this:
                    m_LegalMoveForFirstPlayer = true;
                    //Console.Write(string.Format("{0} is your turn, your sign is (X){1}Please enter point in range or Q for exit game:", m_Game.FirstPlayer.PlayerName, Environment.NewLine));
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);


                    // UpdateBoard
                    ////clear screen
                    //clear pictureBox array
                    ////print update board(Game_load)
                    ////player 2 turn
                  //  manipulateUserChoiceForSecondPlayer();
                }


                player1Turn = false;
                player2Turn = true;
            }


            else if (player2Turn == true)
            {

                if (m_GetUserGameType == (int)eGameMenu.PlayerVsComputer)
                {
                    ////computer turn
                    //// first check if there is legal mooves for computer in all board
                    if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
                    {
                        m_LegalMoveForSecondPlayer = true;
                        m_GameModel.LegalMove(m_GameModel.SecondPlayer);
                        ////clear screen
                        // Screen.Clear();
                        ////print scores and updated board
                        // Console.WriteLine(m_Game.BoardGameCreator);
                        Console.WriteLine(string.Format("{0} score: {1}", m_Game.FirstPlayer.PlayerName, m_Game.FirstPlayer.PlayerScore));
                        Console.WriteLine(string.Format("{0} score: {1}", m_Game.SecondPlayer.PlayerName, m_Game.SecondPlayer.PlayerScore));
                        ////player 2 turn
                      //  ManipulateUserChoiceForFirstPlayer();
                    }
                    else
                    {
                        m_LegalMoveForSecondPlayer = false;
                        if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
                        {
                           // GameOver();
                        }
                        else if (!m_LegalMoveForSecondPlayer)
                        {
                           // Console.WriteLine("Computer out of moves");
                         //   ManipulateUserChoiceForFirstPlayer();
                        }
                    }
                }
                else
                {
                    if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
                    {
                        ////if yes ,dont pick up a flag and do this:
                        m_LegalMoveForSecondPlayer = true;
                     //   Console.Write(string.Format("{0} is your turn, your sign is (O){1}Please enter point in range or Q for exit game:", m_Game.SecondPlayer.PlayerName, Environment.NewLine));
                       // string getPlayerCell = Console.ReadLine();
                        CheckLegalInputCell(getPlayerCell, m_GameModel.SecondPlayer);
                        ////clear screen
                        //  Screen.Clear();
                        ////Print update board
                        //  Console.WriteLine(m_Game.BoardGameCreator);
                      //  Console.WriteLine(string.Format("{0} score: {1}", m_Game.FirstPlayer.PlayerName, m_Game.FirstPlayer.PlayerScore));
                     //   Console.WriteLine(string.Format("{0} score: {1}", m_Game.SecondPlayer.PlayerName, m_Game.SecondPlayer.PlayerScore));
                        ////player 2 turn
                      //  ManipulateUserChoiceForFirstPlayer();
                    }
                    else
                    {
                        m_LegalMoveForSecondPlayer = false;
                        if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
                        {
                           // GameOver();
                        }
                        else if (!m_LegalMoveForSecondPlayer)
                        {
                           // Console.WriteLine(string.Format("{0} you out of moves", m_Game.SecondPlayer.PlayerName));
                           // ManipulateUserChoiceForFirstPlayer();
                        }
                    }
                }
                player1Turn = true;
                player2Turn = false;
            }
        }

        private void Game_Load_1(object sender, EventArgs e)
        {

        }


        private void UpdateBoard(char[] i_charArray, Player i_player)
        {
            
            EnumLettersToNumbers m_ConvertPlayerLetter = new EnumLettersToNumbers();
            bool isLegalMove = false;
            Ex02_Othelo.Point pointToSend = new Ex02_Othelo.Point(pictureLocation.X, pictureLocation.Y);
            isLegalMove = m_GameModel.LegalMove(i_player, pointToSend, eOnlyCheck.No);
            if (!isLegalMove)
            {
               MessageBox.Show("Ilegal move, please choose legal move");
          
            }
        }

        private void GameOver()
        {
            CallTheWinner(m_Game.GetFirstPlayersScore, m_Game.GetSecondPlayersScore);
            Console.WriteLine("Do you wish for another game? Y for yes or Q for exit:");
            string anotherGame = Console.ReadLine();
            bool inCorrectInput = true;
            do
            {
                if (anotherGame.Equals("Y"))
                {
                    m_LegalMoveForFirstPlayer = true;
                    m_LegalMoveForSecondPlayer = true;
                    if (m_GetUserGameType == (int)eGameMenu.PlayerVsComputer)
                    {
                        m_Game = new GameModel(m_Game.Board.Boardsize - 2, m_Game.FirstPlayer.PlayerName, "Computer");
                        //  Screen.Clear();
                        //Console.WriteLine(m_Game.BoardGameCreator);
                        inCorrectInput = false;
                        ManipulateUserChoiceForFirstPlayer();
                    }
                    else
                    {
                        inCorrectInput = false;
                        m_Game = new GameModel(m_Game.Board.Boardsize - 2, m_Game.FirstPlayer.PlayerName, m_Game.SecondPlayer.PlayerName);
                        //   Screen.Clear();
                        //   Console.WriteLine(m_Game.BoardGameCreator);
                        ManipulateUserChoiceForFirstPlayer();
                    }
                }
                else if (anotherGame.Equals("Q"))
                {
                    Console.WriteLine(string.Format("Bye Bye...."));
                    inCorrectInput = false;
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Wrong input!!! please enter one from the following option: Y or Q:");
                    anotherGame = Console.ReadLine();
                }
            }
            while (inCorrectInput);
        }

        private void CallTheWinner(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            StringBuilder callTheWinner = new StringBuilder();
            if (i_FirstPlayerScore > i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, Environment.NewLine));
            }

            if (i_FirstPlayerScore < i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
            }

            if (i_FirstPlayerScore == i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, Draw!!!{4}Scores: {0} : {1} , {2} : {3}{4}", m_Game.SecondPlayer.PlayerName, i_SecondPlayerScore, m_Game.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
            }

            // Console.WriteLine(callTheWinner);
            MessageBox.Show(callTheWinner.ToString());
        }


    }
}
