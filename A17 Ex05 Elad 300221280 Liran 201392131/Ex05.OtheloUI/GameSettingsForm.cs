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
    public partial class GameSettingsForm : Form
    {
        private Button boardSizeButton;
        private Button playerVsComputerButton;
        private Button PlayerVsPlayerButton;
        private int m_BoardSize = 6;
        private Game m_gameForm;

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.boardSizeButton = new System.Windows.Forms.Button();
            this.playerVsComputerButton = new System.Windows.Forms.Button();
            this.PlayerVsPlayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boardSizeButton
            // 
            this.boardSizeButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.boardSizeButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.boardSizeButton.Location = new System.Drawing.Point(43, 39);
            this.boardSizeButton.Name = "boardSizeButton";
            this.boardSizeButton.Size = new System.Drawing.Size(273, 39);
            this.boardSizeButton.TabIndex = 0;
            this.boardSizeButton.Text = "Board size:6x6 (click to increase)";
            this.boardSizeButton.UseVisualStyleBackColor = false;
            this.boardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            this.boardSizeButton.MouseLeave += new System.EventHandler(this.boardSizeButton_MouseLeave);
            this.boardSizeButton.MouseHover += new System.EventHandler(this.boardSizeButton_MouseHover);
            // 
            // playerVsComputerButton
            // 
            this.playerVsComputerButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.playerVsComputerButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.playerVsComputerButton.Location = new System.Drawing.Point(53, 84);
            this.playerVsComputerButton.Name = "playerVsComputerButton";
            this.playerVsComputerButton.Size = new System.Drawing.Size(126, 49);
            this.playerVsComputerButton.TabIndex = 1;
            this.playerVsComputerButton.Text = "Play againts the Computer";
            this.playerVsComputerButton.UseVisualStyleBackColor = false;
            this.playerVsComputerButton.Click += new System.EventHandler(this.playerVsComputerButton_Click);
            this.playerVsComputerButton.MouseLeave += new System.EventHandler(this.playerVsComputerButton_MouseLeave);
            this.playerVsComputerButton.MouseHover += new System.EventHandler(this.playerVsComputerButton_MouseHover);
            // 
            // PlayerVsPlayerButton
            // 
            this.PlayerVsPlayerButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.PlayerVsPlayerButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PlayerVsPlayerButton.Location = new System.Drawing.Point(185, 84);
            this.PlayerVsPlayerButton.Name = "PlayerVsPlayerButton";
            this.PlayerVsPlayerButton.Size = new System.Drawing.Size(120, 49);
            this.PlayerVsPlayerButton.TabIndex = 2;
            this.PlayerVsPlayerButton.Text = "Play againts your friend";
            this.PlayerVsPlayerButton.UseVisualStyleBackColor = false;
            this.PlayerVsPlayerButton.Click += new System.EventHandler(this.PlayerVsPlayerButton_Click);
            this.PlayerVsPlayerButton.MouseLeave += new System.EventHandler(this.PlayerVsPlayerButton_MouseLeave);
            this.PlayerVsPlayerButton.MouseHover += new System.EventHandler(this.PlayerVsPlayerButton_MouseHover);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(353, 166);
            this.Controls.Add(this.PlayerVsPlayerButton);
            this.Controls.Add(this.playerVsComputerButton);
            this.Controls.Add(this.boardSizeButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Setting";
            this.ResumeLayout(false);

        }

        private void boardSizeButton_Click(object sender, EventArgs e)
        {
           if(m_BoardSize == 12)
            {
                m_BoardSize = 6;
            }
           else
            {
                m_BoardSize += 2;
            }
            this.boardSizeButton.Text = string.Format("Board size:{0}x{0} (click to increase)", m_BoardSize);
        }

        private void PlayerVsPlayerButton_Click(object sender, EventArgs e)
        {

            GameController.BoardSize = m_BoardSize;
            GameController.FirstPlayerName = "CoinRed";
            GameController.SecondPlayerName = "CoinYellow";
            GameController.GameType = (int)eGameMenu.PlayerVsPlayer;
            this.Hide();
            this.Close();
            m_gameForm = new Game();
            m_gameForm.ShowDialog();

        }

        private void playerVsComputerButton_Click(object sender, EventArgs e)
        {
            GameController.BoardSize = m_BoardSize;
            GameController.FirstPlayerName = "CoinRed";
            GameController.SecondPlayerName = "Computer";
            GameController.GameType = (int)eGameMenu.PlayerVsComputer;
            this.Hide();
            this.Close();
            m_gameForm = new Game();
            m_gameForm.ShowDialog();
        }

        private void boardSizeButton_MouseHover(object sender, EventArgs e)
        {
            this.boardSizeButton.BackColor = Color.CadetBlue;
        }

        private void boardSizeButton_MouseLeave(object sender, EventArgs e)
        {
            this.boardSizeButton.BackColor = Color.DarkSeaGreen;
        }

        private void PlayerVsPlayerButton_MouseHover(object sender, EventArgs e)
        {
            this.PlayerVsPlayerButton.BackColor = Color.CadetBlue;
        }

        private void PlayerVsPlayerButton_MouseLeave(object sender, EventArgs e)
        {
            this.PlayerVsPlayerButton.BackColor = Color.DarkSeaGreen;
        }

        private void playerVsComputerButton_MouseHover(object sender, EventArgs e)
        {
            this.playerVsComputerButton.BackColor = Color.CadetBlue;
        }

        private void playerVsComputerButton_MouseLeave(object sender, EventArgs e)
        {
            this.playerVsComputerButton.BackColor = Color.DarkSeaGreen;
        }
    }
}
