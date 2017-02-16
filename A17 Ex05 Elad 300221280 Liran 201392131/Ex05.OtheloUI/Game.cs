using System;
using System.Collections.Generic;
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
        private PictureBox[,] pictureBoxArray;
        private GameModel m_GameModel;
        private Ex02_Othelo.Point m_PointForMouseHoverAndLeave;
        private bool player1Turn = true;
        private bool isLegalMoove = false;
        private StringBuilder callTheWinner;
        private bool m_LegalMoveForFirstPlayer = true;
        private bool m_LegalMoveForSecondPlayer = true;
        System.Drawing.Point pictureLocation;
        private Panel panel1;
        List<PictureBox> m_ListOfLegalityBoardPictureBox = new List<PictureBox>();

        public Game()
        {
            m_GameModel = new GameModel(GameController.BoardSize, GameController.FirstPlayerName, GameController.SecondPlayerName);
            m_BoardSize = m_GameModel.Board.Boardsize;
            pictureBoxArray = new PictureBox[m_BoardSize, m_BoardSize];
            pictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);
            InitializeComponent();
            m_GameModel.TrnsferingSignValueUpdate += UpdateCoinToPictureBox;
            m_GameModel.TrnsferingLegalityCellOption += updateLegalityOptionOnPicturBox;
            callTheWinner = new StringBuilder();
            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            // 
            // Game
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.gameLoad);
            this.ResumeLayout(false);

        }


        private void buildBoard()
        {

            for (int i = 1; i < m_BoardSize - 1; i++)
            {
                for (int j = 1; j < m_BoardSize - 1; j++)
                {
                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)(eGameSigns.X))
                    {

                        pictureBoxArray[i, j] = new PictureBox();
                        pictureBoxArray[i, j].Image = Properties.Resources.CoinRed;
                        pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxArray[i, j].Visible = true;
                        pictureBoxArray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.panel1.Controls.Add(pictureBoxArray[i, j]);

                    }

                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.O)
                    {
                        pictureBoxArray[i, j] = new PictureBox();
                        pictureBoxArray[i, j].Image = Properties.Resources.CoinYellow;
                        pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxArray[i, j].Visible = true;
                        pictureBoxArray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.panel1.Controls.Add(pictureBoxArray[i, j]);


                    }

                    else if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.None)
                    {

                        pictureBoxArray[i, j] = new PictureBox() { BackColor = Color.CadetBlue };
                        pictureBoxArray[i, j].Image = Properties.Resources.White;
                        pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxArray[i, j].Visible = true;
                        pictureBoxArray[i, j].Location = pictureLocation;
                        pictureBoxArray[i, j].Name = string.Format("{0}{1}", i, j);
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        pictureBoxArray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.panel1.Controls.Add(pictureBoxArray[i, j]);
                    }

                }

                pictureLocation.X = k_SpaceBetweenPictures;
                pictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;

            }
        }

        private void gameLoad(object sender, EventArgs e)
        {
            switch (GameController.BoardSize)
            {
                case (int)eBoardSize.Six:
                    this.ClientSize = new System.Drawing.Size(400, 400);
                    this.panel1.Size = new System.Drawing.Size(395, 395);
                    break;
                case (int)eBoardSize.Eight:
                    this.ClientSize = new System.Drawing.Size(530, 530);
                    this.panel1.Size = new System.Drawing.Size(525, 525);
                    break;
                case (int)eBoardSize.Ten:
                    this.ClientSize = new System.Drawing.Size(660, 660);
                    this.panel1.Size = new System.Drawing.Size(655, 655);
                    break;
                case (int)eBoardSize.Twelve:
                    this.ClientSize = new System.Drawing.Size(790, 790);
                    this.panel1.Size = new System.Drawing.Size(785, 785);
                    break;
            }
            buildBoard();
            m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer);
        }

        private void clearLegalityMovesList()
        {
            foreach (PictureBox pictureBox in m_ListOfLegalityBoardPictureBox)
            {
                pictureBox.BackColor = Color.CadetBlue;
            }
            m_ListOfLegalityBoardPictureBox.Clear();
        }

        private void pictureBox_Mouseclick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (m_ListOfLegalityBoardPictureBox.Contains(pictureBox))
            {
                //if (!m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer) && !m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer))
                //{
                //    GameOver();
                //}

                //else
               // {

                    if (player1Turn == true)
                    {
                        firstPlayerTurn(pictureBox);
                        clearLegalityMovesList();
                        m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer);
                        this.Show();


                        if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
                        {
                            this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                            computerTurn();
                            clearLegalityMovesList();
                            m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer);
                            this.Show();
                        }


                  //  }
                    else
                    {
                      
                            secondPlayerTurn(pictureBox);
                            clearLegalityMovesList();
                            m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer);
                            this.Show();
                    }
                }
            }
        }

        private void firstPlayerTurn(PictureBox i_PictureBox)
        {
            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
            ////first check if there is legal mooves for the first player in all board
            if (!m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer))////liran ,this is return true all the time...
            {
                m_LegalMoveForFirstPlayer = false;
                MessageBox.Show(string.Format("{0} you out of moves", m_GameModel.FirstPlayer.PlayerName));
                this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                player1Turn = false;

            }

            else
            {
                m_LegalMoveForFirstPlayer = true;
                char[] playerInput;
                playerInput = i_PictureBox.Name.ToCharArray();
                clearLegalityMovesList();
                isLegalMoove = UpdateBoard(playerInput, m_GameModel.FirstPlayer);

                if (isLegalMoove)
                {
                    player1Turn = false;
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                }

                ////player 1 turn again- because of bad legal moove chose
                else
                {
                    player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);

                }
            }

           
        }

        private void secondPlayerTurn(PictureBox i_PictureBox)
        {

            if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
            {
                m_LegalMoveForSecondPlayer = true;
                char[] playerInput;
                playerInput = i_PictureBox.Name.ToCharArray();
                clearLegalityMovesList();
                bool isLegalMoove = UpdateBoard(playerInput, m_GameModel.SecondPlayer);
                if (isLegalMoove)
                {
                    player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                }

                ////player 2 turn again- because of bad legal moove chose
                else
                {
                    player1Turn = false;
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                }
            }

            //no legal moove
            else
            {
                m_LegalMoveForSecondPlayer = false;
                if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
                {
                    GameOver();
                }

                else if (!m_LegalMoveForSecondPlayer)
                {
                    MessageBox.Show(string.Format("{0} out of moves", m_GameModel.SecondPlayer));
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                    player1Turn = true;
                }
            }
        }


        private void computerTurn()
        {
            if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
            {
                m_LegalMoveForSecondPlayer = true;
                clearLegalityMovesList();
                ////liran, this is return false all the time...
                if (m_GameModel.LegalMove(m_GameModel.SecondPlayer))
                {
                    player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);

                }
                player1Turn = true;

            }

            //no legal moove
            else
            {
                m_LegalMoveForSecondPlayer = false;
                if (!m_LegalMoveForFirstPlayer && !m_LegalMoveForSecondPlayer)
                {
                    GameOver();
                }

                else if (!m_LegalMoveForSecondPlayer)
                {
                    MessageBox.Show(string.Format("{0} out of moves", m_GameModel.SecondPlayer));
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                    player1Turn = true;
                }
            }
        }

        private bool UpdateBoard(char[] i_charArray, Player i_player)
        {
            bool isMooveSucceed = true;
            EnumLettersToNumbers m_ConvertPlayerLetter = new EnumLettersToNumbers();
            bool isLegalMove = false;
            Ex02_Othelo.Point pointToSend = new Ex02_Othelo.Point(i_charArray[0] - '0', i_charArray[1] - '0');
            isLegalMove = m_GameModel.LegalMove(i_player, pointToSend, eOnlyCheck.No);
            if (!isLegalMove)
            {
                isMooveSucceed = false;
            }

            return isMooveSucceed;
        }

        private void UpdateCoinToPictureBox(char i_SignValue, Ex02_Othelo.Point i_Point)
        {
            switch (i_SignValue)
            {
                case (char)eGameSigns.X:
                    {
                        pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinRed;
                        break;
                    }
                case (char)eGameSigns.O:
                    {
                        pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinYellow;
                        break;
                    }
            }
        }

        private void GameOver()
        {
            CallTheWinner(m_GameModel.GetFirstPlayersScore, m_GameModel.GetSecondPlayersScore);
        }

        private void updateLegalityOptionOnPicturBox(Ex02_Othelo.Point i_Point)
        {
            m_PointForMouseHoverAndLeave = i_Point;
            pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].BackColor = Color.Green;
            m_ListOfLegalityBoardPictureBox.Add(pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue]);
        }


        private void clearPictureBoxArray()
        {
            for (int i = 1; i < m_BoardSize - 1; i++)
            {
                for (int j = 1; j < m_BoardSize - 1; j++)
                {
                    pictureBoxArray[i, j].Image = null;
                    pictureBoxArray[i, j].Invalidate();

                }
            }

        }

        private void CallTheWinner(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            if (i_FirstPlayerScore > i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                callTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundSecondPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore < i_SecondPlayerScore)
            {
                GameController.GameRoundSecondPlayer++;
                callTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, i_FirstPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundSecondPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore == i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                GameController.GameRoundSecondPlayer++;
                callTheWinner.Append(string.Format("no winners!!! ({0}/{1})({2}/{3}){4}", i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            callTheWinner.Append("Would you like another round?");
            exitApplication();
        }

        public void exitApplication()
        {
            if (MessageBox.Show(callTheWinner.ToString(), "Othello",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.No)
            {
                Application.Exit();
            }

            else
            {
                this.Close();
                this.Hide();
                Game newGame = new Game();
                newGame.ShowDialog();
            }
        }
    }
}