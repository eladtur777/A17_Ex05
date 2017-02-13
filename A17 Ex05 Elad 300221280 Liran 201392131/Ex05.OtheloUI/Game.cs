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
        //const int k_SpaceHightBetweenPicture = 5;
        private int m_BoardSize;
        private PictureBox[,] pictureBoxarray;
        private GameModel m_GameModel;
        PictureBox p;
        public Game()
        {
            m_GameModel = new GameModel(GameController.BoardSize, GameController.FirstPlayerName, GameController.SecondPlayerName);
            m_BoardSize = m_GameModel.Board.Boardsize;
            pictureBoxarray = new PictureBox[m_BoardSize, m_BoardSize];
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(802, 504);
            this.Name = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }



        private void Game_Load(object sender, EventArgs e)
        {
           // int x = 0;
            //int y = 0;

            System.Drawing.Point pictureLocation = new System.Drawing.Point(k_SpaceBetweenPictures, k_SpaceBetweenPictures);

            for (int i = 1; i < m_GameModel.Board.Boardsize - 1; i++)
            {
                for (int j = 1; j < m_GameModel.Board.Boardsize - 1; j++)
                {
                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)(EnumGameSigns.e_Signs.X))
                    {

                        pictureBoxarray[i, j] = new PictureBox();
                        pictureBoxarray[i,j].Image = Properties.Resources.CoinRed;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                       // pictureBoxarray[i, j].Click += pi
                        this.Controls.Add(pictureBoxarray[i, j]);
            
                    }

                    if (m_GameModel.Board.CellBoard[i, j].SignValue == (char)EnumGameSigns.e_Signs.O)
                    {
                        pictureBoxarray[i, j] = new PictureBox();
                        pictureBoxarray[i, j].Image = Properties.Resources.CoinYellow;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        // pictureBoxarray[i, j].Click += pi
                        this.Controls.Add(pictureBoxarray[i, j]);
       

                    }

                    else if(m_GameModel.Board.CellBoard[i, j].SignValue == (char)EnumGameSigns.e_Signs.None)
                    {

                        pictureBoxarray[i, j] = new PictureBox() { BackColor = Color.Red };
                        pictureBoxarray[i, j].Image = Properties.Resources.White;
                        pictureBoxarray[i, j].Margin = new Padding(k_SpaceBetweenPictures);
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = pictureLocation;
                        pictureLocation.X += k_PictureBoxArea + k_SpaceBetweenPictures;
                        pictureBoxarray[i, j].Size = new Size(k_PictureBoxArea, k_PictureBoxArea);
                        // pictureBoxarray[i, j].Click += pi
                        this.Controls.Add(pictureBoxarray[i, j]);
          

                    }

           
                }


                //  pictureLocation.X += k_SpaceBetweenPictures;
                pictureLocation.Y += k_PictureBoxArea + k_SpaceBetweenPictures;




            }

        }

        private void Game_Load_1(object sender, EventArgs e)
        {

        }
    }
}
