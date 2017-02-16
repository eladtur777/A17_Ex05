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
        private const int k_PictureBoxArea = 60;
        private const int k_SpaceBetweenPictures = 4;
        private int m_BoardSize;
        private PictureBox[,] pictureBoxArray;
        private GameModel m_GameModel;
        private Ex02_Othelo.Point m_PointForMouseHoverAndLeave;
        private bool player1Turn = true;
        private StringBuilder callTheWinner;
        private bool m_LegalMoveForFirstPlayer = true;
        private bool m_LegalMoveForSecondPlayer = true;
        private System.Drawing.Point pictureLocation;
        private Panel panel1;
        private List<PictureBox> m_ListOfLegalityBoardPictureBox = new List<PictureBox>();

        public Game()
        {
            this.m_GameModel = new GameModel(GameController.BoardSize, GameController.FirstPlayerName, GameController.SecondPlayerName);
            this.m_BoardSize = this.m_GameModel.Board.Boardsize;
            this.pictureBoxArray = new PictureBox[this.m_BoardSize, this.m_BoardSize];
            this.pictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);
            this.InitializeComponent();
            this.m_GameModel.TrnsferingSignValueUpdate += this.updateCoinToPictureBox;
            this.m_GameModel.TrnsferingLegalityCellOption += this.updateLegalityOptionOnPicturBox;
            this.callTheWinner = new StringBuilder();
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //// 
            ////panel1
            //// 
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            //// 
            //// Game
            //// 
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
            for (int i = 1; i < this.m_BoardSize - 1; i++)
            {
                for (int j = 1; j < this.m_BoardSize - 1; j++)
                {
                    if (this.m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.X)
                    {
                        this.pictureBoxArray[i, j] = new PictureBox();
                        this.pictureBoxArray[i, j].Image = Properties.Resources.CoinRed;
                        this.pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.pictureBoxArray[i, j].Visible = true;
                        this.pictureBoxArray[i, j].Location = this.pictureLocation;
                        this.pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.panel1.Controls.Add(this.pictureBoxArray[i, j]);
                    }

                    if (this.m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.O)
                    {
                        this.pictureBoxArray[i, j] = new PictureBox();
                        this.pictureBoxArray[i, j].Image = Properties.Resources.CoinYellow;
                        this.pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.pictureBoxArray[i, j].Visible = true;
                        this.pictureBoxArray[i, j].Location = this.pictureLocation;
                        this.pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.panel1.Controls.Add(this.pictureBoxArray[i, j]);
                    }
                    else if (this.m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.None)
                    {
                        this.pictureBoxArray[i, j] = new PictureBox() { BackColor = Color.CadetBlue };
                        this.pictureBoxArray[i, j].Image = Properties.Resources.White;
                        this.pictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.pictureBoxArray[i, j].Visible = true;
                        this.pictureBoxArray[i, j].Location = this.pictureLocation;
                        this.pictureBoxArray[i, j].Name = string.Format("{0}{1}", i, j);
                        this.pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.pictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.pictureBoxArray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.panel1.Controls.Add(this.pictureBoxArray[i, j]);
                    }
                }

                this.pictureLocation.X = k_SpaceBetweenPictures;
                this.pictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;
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

            this.buildBoard();
            this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer);
        }

        private void clearLegalityMovesList()
        {
            foreach (PictureBox pictureBox in this.m_ListOfLegalityBoardPictureBox)
            {
                pictureBox.BackColor = Color.CadetBlue;
            }

            this.m_ListOfLegalityBoardPictureBox.Clear();
        }

        private void pictureBox_Mouseclick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (this.m_ListOfLegalityBoardPictureBox.Contains(pictureBox))
            {
                if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer) && !this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                {
                    this.GameOver();
                }
                else
                {
                    if (this.player1Turn == true)
                    {
                        this.firstPlayerTurn(pictureBox);
                        this.clearLegalityMovesList();
                        this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer);
                        this.Show();
                    }
                    else
                    {
                        this.secondPlayerTurn(pictureBox);
                        this.clearLegalityMovesList();
                        this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer);
                        this.Show();
                    }
                }
            }
        }

        private void firstPlayerTurn(PictureBox i_PictureBox)
        {
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
            ////first check if there is legal mooves for the first player in all board
            if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))////liran ,this is return true all the time...
            {
                this.m_LegalMoveForFirstPlayer = false;
                MessageBox.Show(string.Format("{0} you out of moves", this.m_GameModel.FirstPlayer.PlayerName));
                this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
                this.player1Turn = false;
            }
            else
            {
                this.m_LegalMoveForFirstPlayer = true;
                char[] playerInput;
                playerInput = i_PictureBox.Name.ToCharArray();
                this.clearLegalityMovesList();
                bool isLegalMoove = this.UpdateBoard(playerInput, this.m_GameModel.FirstPlayer);

                if (isLegalMoove)
                {
                    this.player1Turn = false;
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);

                    if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
                    {
                        this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
                        this.computerTurn(i_PictureBox);
                    }
                }
                else
                {
                    ////player 1 turn again- because of bad legal moove chose
                    this.player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                }
            }
        }

        private void secondPlayerTurn(PictureBox i_PictureBox)
        {
            if (this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
            {
                this.m_LegalMoveForSecondPlayer = true;
                char[] playerInput;
                playerInput = i_PictureBox.Name.ToCharArray();
                this.clearLegalityMovesList();
                bool isLegalMoove = this.UpdateBoard(playerInput, this.m_GameModel.SecondPlayer);
                if (isLegalMoove)
                {
                    this.player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                }
                else
                {
                    ////player 2 turn again- because of bad legal moove chose
                    this.player1Turn = false;
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
                }
            }
            else
            {
                ////no legal moove
                this.m_LegalMoveForSecondPlayer = false;
                if (!this.m_LegalMoveForFirstPlayer && !this.m_LegalMoveForSecondPlayer)
                {
                    this.GameOver();
                }
                else if (!this.m_LegalMoveForSecondPlayer)
                {
                    MessageBox.Show(string.Format("{0} out of moves", this.m_GameModel.SecondPlayer));
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                    this.player1Turn = true;
                }
            }
        }

        private void computerTurn(PictureBox i_PictureBox)
        {
            if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
            {
                this.m_LegalMoveForSecondPlayer = true;
                this.clearLegalityMovesList();
                ////liran, this is return false all the time...
                if (this.m_GameModel.LegalMove(this.m_GameModel.SecondPlayer))
                {
                    player1Turn = true;
                    this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                }
            }
            else
            {
                ////no legal moove
                this.m_LegalMoveForSecondPlayer = false;
                if (!this.m_LegalMoveForFirstPlayer && !this.m_LegalMoveForSecondPlayer)
                {
                    this.GameOver();
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

        private void updateCoinToPictureBox(char i_SignValue, Ex02_Othelo.Point i_Point)
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