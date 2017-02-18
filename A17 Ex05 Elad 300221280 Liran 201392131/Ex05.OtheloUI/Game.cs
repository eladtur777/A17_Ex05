using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex02_Othelo;
using System.Diagnostics;

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
        private bool playerTurn = true;
        private StringBuilder callTheWinner;
        private System.Drawing.Point pictureLocation;
        private Panel panel1;
        private List<PictureBox> m_ListOfLegalityBoardPictureBox = new List<PictureBox>();
        private Timer m_Timer = new Timer();

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
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
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
                        this.pictureBoxArray[i, j].Name = string.Format("{0},{1}", i, j);
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
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.panel1.Size = new System.Drawing.Size(395, 395);
                    break;
                case (int)eBoardSize.Eight:
                    this.ClientSize = new System.Drawing.Size(530, 530);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.panel1.Size = new System.Drawing.Size(525, 525);
                    break;
                case (int)eBoardSize.Ten:
                    this.ClientSize = new System.Drawing.Size(660, 660);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.panel1.Size = new System.Drawing.Size(655, 655);
                    break;
                case (int)eBoardSize.Twelve:
                    this.ClientSize = new System.Drawing.Size(790, 790);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
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
                if (this.playerTurn)
                {
                    this.firstPlayerTurn(pictureBox);
                    if (GameController.GameType == (int)eGameMenu.PlayerVsPlayer)
                    {
                        if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                        {
                            if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                            {
                                this.GameOver();
                            }
                            else
                            {
                                this.playerTurn = true;
                            }

                            MessageBox.Show(string.Format("No legals mooves for {0}", this.m_GameModel.SecondPlayer.PlayerName));
                            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                        }
                    }
                    else if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
                    {
                        if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                        {
                            if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                            {
                                this.GameOver();
                            }
                            else
                            {
                                this.computerTurn();
                                this.playerTurn = true;
                                this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer);
                            }
                        }
                    }

                    this.Show();
                }
                else
                {
                    this.secondPlayerTurn(pictureBox);
                    this.clearLegalityMovesList();
                    if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                    {
                        if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                        {
                            this.GameOver();
                        }
                        else
                        {
                            this.playerTurn = false;
                        }

                        MessageBox.Show(string.Format("No legals mooves for {0}", this.m_GameModel.FirstPlayer.PlayerName));
                        this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
                    }

                    this.Show();
                }
            }
            else
            {
               // MessageBox.Show("Please Choose legal moove button (green buttons for possible legal mooves)");
            }
        }

        private void firstPlayerTurn(PictureBox i_PictureBox)
        {
            string[] coordinates = this.parserPictureBoxCoordinates(i_PictureBox);
            this.clearLegalityMovesList();
            this.UpdateBoard(coordinates, this.m_GameModel.FirstPlayer);
            this.playerTurn = false;
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);

            if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
            {
                this.panel1.Enabled =false;
                m_Timer.Interval = (1000);
                m_Timer.Tick += new EventHandler(timerTick);
                m_Timer.Enabled = true;
                m_Timer.Start();

                if (this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                    {
                        this.computerTurn();
                }

                this.playerTurn = true;
                this.Show();

            }
        }

        private void secondPlayerTurn(PictureBox i_PictureBox)
        {
            {
                string[] coordinates = this.parserPictureBoxCoordinates(i_PictureBox);
                this.clearLegalityMovesList();
                this.UpdateBoard(coordinates, this.m_GameModel.SecondPlayer);
                this.playerTurn = true;
                this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
            }
        }

        private void computerTurn()
        {

            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
            this.Show();
            this.m_GameModel.LegalMove(this.m_GameModel.SecondPlayer);
            this.clearLegalityMovesList();
        }

        private void timerTick(object sender, EventArgs e)
        {
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
            m_Timer.Stop();
            this.panel1.Enabled = true;
        }

        private string[] parserPictureBoxCoordinates(PictureBox i_PictureBox)
        {
            char[] playerInput = { ' ', ',', ' ' };
            string[] coordinates = i_PictureBox.Name.Split(playerInput);
            return coordinates;
        }

        private bool UpdateBoard(string[] i_Coordinate, Player i_player)
        {
            int countCoordinates = 0;
            int firstCoordinate = 0;
            int secondCoordinate = 0;
            bool isMooveSucceed = true;
            bool isLegalMove = false;

            foreach (string coordinate in i_Coordinate)
            {
                if (countCoordinates == 0)
                {
                    firstCoordinate = int.Parse(coordinate);
                }

                countCoordinates++;

                if (countCoordinates == 2)
                {
                    secondCoordinate = int.Parse(coordinate);
                }
            }

            Ex02_Othelo.Point pointToSend = new Ex02_Othelo.Point(firstCoordinate, secondCoordinate);
            isLegalMove = this.m_GameModel.LegalMove(i_player, pointToSend, eOnlyCheck.No);
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
                        this.pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinRed;
                        break;
                    }

                case (char)eGameSigns.O:
                    {
                        this.pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinYellow;
                        break;
                    }
            }
        }

        private void GameOver()
        {
            this.CallTheWinner(this.m_GameModel.GetFirstPlayersScore, this.m_GameModel.GetSecondPlayersScore);
        }

        private void updateLegalityOptionOnPicturBox(Ex02_Othelo.Point i_Point)
        {
            this.m_PointForMouseHoverAndLeave = i_Point;
            this.pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].BackColor = Color.Green;
            this.m_ListOfLegalityBoardPictureBox.Add(this.pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue]);
        }

        private void clearPictureBoxArray()
        {
            for (int i = 1; i < this.m_BoardSize - 1; i++)
            {
                for (int j = 1; j < this.m_BoardSize - 1; j++)
                {
                    this.pictureBoxArray[i, j].Image = null;
                    this.pictureBoxArray[i, j].Invalidate();
                }
            }
        }

        private void CallTheWinner(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            if (i_FirstPlayerScore > i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                this.callTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", this.m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundSecondPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore < i_SecondPlayerScore)
            {
                GameController.GameRoundSecondPlayer++;
                this.callTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", this.m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, i_FirstPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundSecondPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore == i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                GameController.GameRoundSecondPlayer++;
                this.callTheWinner.Append(string.Format("No winners!!! ({0}/{1})({2}/{3}){4}", i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            this.callTheWinner.Append("Would you like another round?");
            this.exitApplication();
        }

        private void exitApplication()
        {
            if (MessageBox.Show(this.callTheWinner.ToString(), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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