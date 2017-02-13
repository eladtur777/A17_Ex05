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
        private PictureBox[,] pictureBoxarray;
        private Board buildBoard;
        private GameModel gameModel;

        public Game()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.ClientSize = new System.Drawing.Size(333, 288);
            this.Name = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Game_Load(object sender, EventArgs e)
        {
            int x = 45;
            int y = 45;
            string img1 = @"CoinsImages\CoinRed.png";
            string img2 = @"CoinsImages\CoinYellow.png";
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (buildBoard.CellBoard[i, j].Equals(EnumGameSigns.e_Signs.X))
                    {

                        //Size = new Size(50, 50),
                        //Location = new System.Drawing.Point(x, y),
                        //ImageLocation = @"CoinsImages\CoinRed.png",
                        //// SizeMode = PictureBoxSizeMode.CenterImage

                        pictureBoxarray[i,j] = new PictureBox();
                        pictureBoxarray[i, j].ImageLocation = img1;
                    //    pictureBoxarray[i,j].Image = Properties.Resources.img1;
                        pictureBoxarray[i,j].Visible = true;
                        pictureBoxarray[i,j].Location = new System.Drawing.Point(x, y);
                      //  this.Size = new Size(250, 250);
                        pictureBoxarray[i,j].Size = new Size(50, 50);
                        this.Controls.Add(pictureBoxarray[i,j]);

                    }

                    if (buildBoard.CellBoard[i, j].Equals(EnumGameSigns.e_Signs.O))
                    {
                        pictureBoxarray[i, j] = new PictureBox();
                        pictureBoxarray[i, j].ImageLocation = img2;
                        //    pictureBoxarray[i,j].Image = Properties.Resources.img1;
                        pictureBoxarray[i, j].Visible = true;
                        pictureBoxarray[i, j].Location = new System.Drawing.Point(x, y);
                        //  this.Size = new Size(250, 250);
                        pictureBoxarray[i, j].Size = new Size(50, 50);
                        this.Controls.Add(pictureBoxarray[i, j]);
                    }

                }



            }





        }
    }
}
