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
        private PictureBox[,] pictureBoxArray;
        private GameModel m_GameModel;
        private bool player1Turn = true;
        private bool player2Turn = false;
        private bool m_LegalMoveForFirstPlayer = true;
        private bool m_LegalMoveForSecondPlayer = true;
        System.Drawing.Point pictureLocation;
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
        }

        private void InitializeComponent()
        {
           // this.SuspendLayout();
            // 
            // Game
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.ShowIcon = false;
            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }


        private void buildBoard()
        {
            //Array.Clear(pictureBoxarray,1,m_BoardSize);

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
                        //    pictureBoxarray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.Controls.Add(pictureBoxArray[i, j]);

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
                        //  pictureBoxarray[i, j].Click += new EventHandler(this.pictureBox_Mouseclick);
                        this.Controls.Add(pictureBoxArray[i, j]);


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
                        // pictureBoxArray[i, j].MouseHover += new EventHandler(this.pictureBox_MouseHover);
                        //  pictureBoxArray[i, j].MouseLeave += new EventHandler(this.pictureBox_MouseLeave);
                        this.Controls.Add(pictureBoxArray[i, j]);
                    }

                }

                pictureLocation.X = k_SpaceBetweenPictures;
                pictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;

            }

        }

        private void Game_Load(object sender, EventArgs e)
        {
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
            buildBoard();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.BackColor = Color.CadetBlue;
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.BackColor = Color.Coral;
        }

        /// <summary>
        /// on mouse click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_Mouseclick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (player1Turn == true)
            {
                this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                ////first check if there is legal mooves for the first player in all board
                if (!m_GameModel.ThereIsExisitingLegalMove(m_GameModel.FirstPlayer))
                {
                    m_LegalMoveForFirstPlayer = false;
                    MessageBox.Show(string.Format("{0} you out of moves", m_GameModel.FirstPlayer.PlayerName));
                    this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                    player1Turn = false;
                    player2Turn = true;

                }

                else
                {
                    ///TODO need to add here new draw of board function , all legality options should appear in green color 
                    ////if yes ,dont pick up a flag and do this:
                    //m_LegalMoveForFirstPlayer = true;

                    //foreach (PictureBox pictureBox in m_ListOfLegalityBoardPictureBox)
                    //{
                    //    pictureBox.Image = Properties.Resources.White;
                    //}
                    //m_ListOfLegalityBoardPictureBox.Clear();

                    // this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                    char[] playerInput;
                    playerInput = pb.Name.ToCharArray();
                    bool isLegalMoove = UpdateBoard(playerInput, m_GameModel.FirstPlayer);
                    ///TODO need to add here new draw of board function ,all move appears here correctly 
                    if (isLegalMoove)
                    {

                        ////player 2 turn
                        player1Turn = false;
                        player2Turn = true;
                        this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                    //    clearPictureBoxArray();
                        buildBoard();

                    }
                    ////player 1 turn again- because of bad legal moove chose
                    else
                    {
                        player1Turn = true;
                        player2Turn = false;
                        this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                        // buildBoard();

                    }


                }
            }


             if (player2Turn == true)
            {
              //  this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
                {
                    ////computer turn
                    //// first check if there is legal mooves for computer in all board
                    if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
                    {
                        // this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                        m_LegalMoveForSecondPlayer = true;
                        m_GameModel.LegalMove(m_GameModel.SecondPlayer);
                        buildBoard();
                        ////player 1 turn
                        player1Turn = true;
                        player2Turn = false;
                        this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
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
                            MessageBox.Show("Computer out of moves");
                            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                            player1Turn = true;
                            player2Turn = false;
                        }
                    }
                }
                ////PlayervsPlayer
                else
                {
                    if (m_GameModel.ThereIsExisitingLegalMove(m_GameModel.SecondPlayer))
                    {
                        ////if yes ,dont pick up a flag and do this:
                        m_LegalMoveForSecondPlayer = true;
                        //foreach (PictureBox pictureBox in m_ListOfLegalityBoardPictureBox)
                        //{
                        //    pictureBox.Image = Properties.Resources.White;
                        //}
                        //m_ListOfLegalityBoardPictureBox.Clear();
                        //  this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);
                        char[] playerInput;
                        playerInput = pb.Name.ToCharArray();
                        bool isLegalMoove = UpdateBoard(playerInput, m_GameModel.SecondPlayer);

                        if (isLegalMoove)
                        {
                            player1Turn = true;
                            player2Turn = false;
                            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                            buildBoard();

                        }

                        else
                        {
                            player1Turn = false;
                            player2Turn = true;
                            this.Text = string.Format("Othello - {0} turn", m_GameModel.SecondPlayer.PlayerName);

                        }
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
                            MessageBox.Show(string.Format("{0} out of moves", m_GameModel.SecondPlayer));
                            this.Text = string.Format("Othello - {0} turn", m_GameModel.FirstPlayer.PlayerName);
                            player1Turn = true;
                            player2Turn = false;
                        }
                    }
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
               // MessageBox.Show("Ilegal move, please choose legal move");
                isMooveSucceed = false;

            }

            return isMooveSucceed;
        }

        private void UpdateCoinToPictureBox(char i_SignValue, Ex02_Othelo.Point i_Point)
        {
            switch(i_SignValue)
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

        /// <summary>
        /// not fixed
        /// </summary>
        private void GameOver()
        {
            CallTheWinner(m_GameModel.GetFirstPlayersScore, m_GameModel.GetSecondPlayersScore);
            //Console.WriteLine("Do you wish for another game? Y for yes or Q for exit:");
            //string anotherGame = Console.ReadLine();
            //bool inCorrectInput = true;
            //do
            //{
            //    if (anotherGame.Equals("Y"))
            //    {
            //        m_LegalMoveForFirstPlayer = true;
            //        m_LegalMoveForSecondPlayer = true;
            //        if (GameController.GameType == (int)eGameMenu.PlayerVsComputer)
            //        {
            //            m_GameModel = new GameModel(m_GameModel.Board.Boardsize - 2, m_GameModel.FirstPlayer.PlayerName, "Computer");
            //            //  Screen.Clear();
            //            //Console.WriteLine(m_Game.BoardGameCreator);
            //            inCorrectInput = false;
            //          //  ManipulateUserChoiceForFirstPlayer();
            //        }
            //        else
            //        {
            //            inCorrectInput = false;
            //            m_GameModel = new GameModel(m_GameModel.Board.Boardsize - 2, m_GameModel.FirstPlayer.PlayerName, m_GameModel.SecondPlayer.PlayerName);
            //            //   Screen.Clear();
            //            //   Console.WriteLine(m_Game.BoardGameCreator);
            //           // ManipulateUserChoiceForFirstPlayer();
            //        }
            //    }
            //else if (anotherGame.Equals("Q"))
            //{
            //    Console.WriteLine(string.Format("Bye Bye...."));
            //    inCorrectInput = false;
            //    Environment.Exit(0);
            //}
            //else
            //{
            //    Console.WriteLine("Wrong input!!! please enter one from the following option: Y or Q:");
            //    anotherGame = Console.ReadLine();
            //}
            //}
            //while (inCorrectInput);
        }

        private void updateLegalityOptionOnPicturBox(Ex02_Othelo.Point i_Point)
        {
                    //  pictureBoxArray[i_Point.AxisXValue, i_Point.AxisYValue].Image = Properties.Resources.White;// Elad change it to Green
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
            StringBuilder callTheWinner = new StringBuilder();
            if (i_FirstPlayerScore > i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, Environment.NewLine));
            }

            if (i_FirstPlayerScore < i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, The Winner is: {0}{4}Scores: {0} : {1} , {2} : {3}{4}", m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
            }

            if (i_FirstPlayerScore == i_SecondPlayerScore)
            {
                callTheWinner.Append(string.Format("Game Over, Draw!!!{4}Scores: {0} : {1} , {2} : {3}{4}", m_GameModel.SecondPlayer.PlayerName, i_SecondPlayerScore, m_GameModel.FirstPlayer.PlayerName, i_FirstPlayerScore, Environment.NewLine));
            }

            callTheWinner.Append("Would you like another round?");
            // Console.WriteLine(callTheWinner);
            DialogResult result = MessageBox.Show(callTheWinner.ToString(), "Othello", MessageBoxButtons.YesNo);
            if (result == DialogResult.OK)
            {

            }

            else
            {
                this.Close();
                this.Hide();
            }
        }
    }
}