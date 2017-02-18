using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex02_Othelo;

namespace Ex05.OtheloUI
{
    public partial class GameForm : Form
    {
        private const int k_PictureBoxArea = 60;
        private const int k_SpaceBetweenPictures = 4;
        private int m_BoardSize;
        private PictureBox[,] m_PictureBoxArray;
        private GameModel m_GameModel;
        private Ex02_Othelo.Point m_PointForMouseHoverAndLeave;
        private bool m_IsFirstPlayerTurn = true;
        private StringBuilder m_CallTheWinner;
        private System.Drawing.Point m_PictureLocation;
        private Panel m_PictureBoxPanel;
        private List<PictureBox> m_ListOfLegalityBoardPictureBox = new List<PictureBox>();

        public GameForm()
        {
            this.m_GameModel = new GameModel(GameController.BoardSize, GameController.FirstPlayerName, GameController.SecondPlayerName);
            this.m_BoardSize = this.m_GameModel.Board.Boardsize;
            this.m_PictureBoxArray = new PictureBox[this.m_BoardSize, this.m_BoardSize];
            this.m_PictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);
            this.initializeComponent();
            this.m_GameModel.SignValueTrnsferingFromLogicToUI += this.updateCoinToPictureBox_signValueTrnsferingFromLogicToUI;
            this.m_GameModel.LegalCellCoordinatesTransferingFromLogicToUI += this.updateLegalityOptionOnPicturBox_legalCellCoordinatesTransferingFromLogicToUI;
            this.m_CallTheWinner = new StringBuilder();
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
        }

        private void initializeComponent()
        {
            this.m_PictureBoxPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            this.m_PictureBoxPanel.Location = new System.Drawing.Point(6, 6);
            this.m_PictureBoxPanel.Name = "pictureBoxPanel";
            this.m_PictureBoxPanel.Size = new System.Drawing.Size(200, 100);
            this.m_PictureBoxPanel.TabIndex = 0;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.m_PictureBoxPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.formLoading_Load);
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
                        this.m_PictureBoxArray[i, j] = new PictureBox();
                        this.m_PictureBoxArray[i, j].Image = Properties.Resources.CoinRed;
                        this.m_PictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.m_PictureBoxArray[i, j].Visible = true;
                        this.m_PictureBoxArray[i, j].Location = this.m_PictureLocation;
                        this.m_PictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.m_PictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.m_PictureBoxPanel.Controls.Add(this.m_PictureBoxArray[i, j]);
                    }

                    if (this.m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.O)
                    {
                        this.m_PictureBoxArray[i, j] = new PictureBox();
                        this.m_PictureBoxArray[i, j].Image = Properties.Resources.CoinYellow;
                        this.m_PictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.m_PictureBoxArray[i, j].Visible = true;
                        this.m_PictureBoxArray[i, j].Location = this.m_PictureLocation;
                        this.m_PictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.m_PictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.m_PictureBoxPanel.Controls.Add(this.m_PictureBoxArray[i, j]);
                    }
                    else if (this.m_GameModel.Board.CellBoard[i, j].SignValue == (char)eGameSigns.None)
                    {
                        this.m_PictureBoxArray[i, j] = new PictureBox() { BackColor = Color.CadetBlue };
                        this.m_PictureBoxArray[i, j].Image = Properties.Resources.White;
                        this.m_PictureBoxArray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        this.m_PictureBoxArray[i, j].Visible = true;
                        this.m_PictureBoxArray[i, j].Location = this.m_PictureLocation;
                        this.m_PictureBoxArray[i, j].Name = string.Format("{0},{1}", i, j);
                        this.m_PictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        this.m_PictureBoxArray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        this.m_PictureBoxArray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.m_PictureBoxPanel.Controls.Add(this.m_PictureBoxArray[i, j]);
                    }
                }

                this.m_PictureLocation.X = k_SpaceBetweenPictures;
                this.m_PictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;
            }
        }

        private void formLoading_Load(object i_Sender, EventArgs i_EventArgs)
        {
            switch (GameController.BoardSize)
            {
                case (int)eBoardSize.Six:
                    this.ClientSize = new System.Drawing.Size(400, 400);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.m_PictureBoxPanel.Size = new System.Drawing.Size(395, 395);
                    break;
                case (int)eBoardSize.Eight:
                    this.ClientSize = new System.Drawing.Size(530, 530);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.m_PictureBoxPanel.Size = new System.Drawing.Size(525, 525);
                    break;
                case (int)eBoardSize.Ten:
                    this.ClientSize = new System.Drawing.Size(660, 660);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.m_PictureBoxPanel.Size = new System.Drawing.Size(655, 655);
                    break;
                case (int)eBoardSize.Twelve:
                    this.ClientSize = new System.Drawing.Size(790, 790);
                    this.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (this.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (this.Size.Height / 2));
                    this.m_PictureBoxPanel.Size = new System.Drawing.Size(785, 785);
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

        private void pictureBox_Mouseclick(object i_Sender, EventArgs i_EventArgs)
        {
            PictureBox pictureBox = (PictureBox)i_Sender;
            if (this.m_ListOfLegalityBoardPictureBox.Contains(pictureBox))
            {
                if (this.m_IsFirstPlayerTurn)
                {
                    this.firstPlayerTurn(pictureBox);
                    if (GameController.GameType == (int)eGameMenu.PlayerVsPlayer)
                    {
                        if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                        {
                            if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                            {
                                this.gameOver();
                            }
                            else
                            {
                                this.m_IsFirstPlayerTurn = true;
                                MessageBox.Show(string.Format("No legals move for {0}", this.m_GameModel.SecondPlayer.PlayerName));
                            }

                            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                        }
                    }
                    else if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
                    {
                        while (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                        {
                            if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                            {
                                this.gameOver();
                                break;
                            }
                            else
                            {
                                this.computerTurn();
                                this.m_IsFirstPlayerTurn = true;
                            }
                        }

                        this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
                        this.Show();
                    }

                    this.Show();
                    this.m_PictureBoxPanel.Enabled = true;
                }
                else
                {
                    this.secondPlayerTurn(pictureBox);
                    this.clearLegalityMovesList();
                    if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.FirstPlayer))
                    {
                        if (!this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                        {
                            this.gameOver();
                        }
                        else
                        {
                            this.m_IsFirstPlayerTurn = false;
                            MessageBox.Show(string.Format("No legals move for {0}", this.m_GameModel.FirstPlayer.PlayerName));
                        }

                        this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
                    }
  
                    this.Show();
                }
            }
        }

        private void firstPlayerTurn(PictureBox i_PictureBox)
        {
            string[] coordinates = this.parserPictureBoxCoordinates(i_PictureBox);
            this.clearLegalityMovesList();
            this.updateBoard(coordinates, this.m_GameModel.FirstPlayer);
            this.m_IsFirstPlayerTurn = false;
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);

            if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
            {
                this.m_PictureBoxPanel.Enabled = false;

                if (this.m_GameModel.ThereIsExisitingLegalMove(this.m_GameModel.SecondPlayer))
                {
                        this.computerTurn();
                }
       
                this.m_IsFirstPlayerTurn = true;
                this.Show();
            }
        }

        private void secondPlayerTurn(PictureBox i_PictureBox)
        {
                string[] coordinates = this.parserPictureBoxCoordinates(i_PictureBox);
                this.clearLegalityMovesList();
                this.updateBoard(coordinates, this.m_GameModel.SecondPlayer);
                this.m_IsFirstPlayerTurn = true;
                this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
        }

        private void computerTurn()
        {
            this.m_PictureBoxPanel.Enabled = false;
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.SecondPlayer.PlayerName);
            this.Show();
            MessageBox.Show("Computer turn", "Othello", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.m_GameModel.LegalMove(this.m_GameModel.SecondPlayer);
            this.clearLegalityMovesList();
            this.Text = string.Format("Othello - {0} turn", this.m_GameModel.FirstPlayer.PlayerName);
        }

        private string[] parserPictureBoxCoordinates(PictureBox i_PictureBox)
        {
            char[] playerInput = { ' ', ',', ' ' };
            string[] coordinates = i_PictureBox.Name.Split(playerInput);
            return coordinates;
        }

        private bool updateBoard(string[] i_Coordinate, Player i_player)
        {
            int countCoordinates = 0;
            int firstCoordinate = 0;
            int secondCoordinate = 0;
            bool isMoveSucceed = true;
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
                isMoveSucceed = false;
            }

            return isMoveSucceed;
        }

        private void updateCoinToPictureBox_signValueTrnsferingFromLogicToUI(char i_SignValue, Ex02_Othelo.Point i_Point)
        {
            switch (i_SignValue)
            {
                case (char)eGameSigns.X:
                    {
                        this.m_PictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinRed;
                        break;
                    }

                case (char)eGameSigns.O:
                    {
                        this.m_PictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.CoinYellow;
                        break;
                    }
            }
        }

        private void gameOver()
        {
            this.callTheWinner(this.m_GameModel.GetFirstPlayersScore, this.m_GameModel.GetSecondPlayersScore);
        }

        private void updateLegalityOptionOnPicturBox_legalCellCoordinatesTransferingFromLogicToUI(Ex02_Othelo.Point i_Point)
        {
            this.m_PointForMouseHoverAndLeave = i_Point;
            this.m_PictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].BackColor = Color.Green;
            this.m_ListOfLegalityBoardPictureBox.Add(this.m_PictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue]);
        }

        private void clearPictureBoxArray()
        {
            for (int i = 1; i < this.m_BoardSize - 1; i++)
            {
                for (int j = 1; j < this.m_BoardSize - 1; j++)
                {
                    this.m_PictureBoxArray[i, j].Image = null;
                    this.m_PictureBoxArray[i, j].Invalidate();
                }
            }
        }

        private void callTheWinner(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            if (i_FirstPlayerScore > i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                this.m_CallTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", this.m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundSecondPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore < i_SecondPlayerScore)
            {
                GameController.GameRoundSecondPlayer++;
                this.m_CallTheWinner.Append(string.Format("{0} Won!!! ({1}/{2})({3}/{4}){5}", this.m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, i_FirstPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundSecondPlayer, Environment.NewLine));
            }

            if (i_FirstPlayerScore == i_SecondPlayerScore)
            {
                GameController.GameRoundFirstPlayer++;
                GameController.GameRoundSecondPlayer++;
                this.m_CallTheWinner.Append(string.Format("No winners!!! ({0}/{1})({2}/{3}){4}", i_FirstPlayerScore, i_SecondPlayerScore, GameController.GameRoundFirstPlayer, GameController.GameRoundFirstPlayer, Environment.NewLine));
            }

            this.m_CallTheWinner.Append("Would you like another round?");
            this.exitApplication();
        }

        private void exitApplication()
        {
            if (MessageBox.Show(this.m_CallTheWinner.ToString(), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                Application.Exit();
            }
            else
            {
                this.Close();
                this.Hide();
                GameForm newGame = new GameForm();
                newGame.ShowDialog();
            }
        }
    }
}