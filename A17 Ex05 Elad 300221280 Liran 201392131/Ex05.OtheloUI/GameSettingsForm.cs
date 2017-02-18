using System;
using System.Drawing;
using System.Windows.Forms;
using Ex02_Othelo;

namespace Ex05.OtheloUI
{
    public partial class GameSettingsForm : Form
    {
        private Button m_BoardSizeButton;
        private Button m_PlayerVsComputerButton;
        private Button m_PlayerVsPlayerButton;
        private int m_BoardSize = 6;
        private GameForm m_GameForm;

        public GameSettingsForm()
        {
            this.initializeComponent();
        }

        private void initializeComponent()
        {
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_PlayerVsComputerButton = new System.Windows.Forms.Button();
            this.m_PlayerVsPlayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.m_BoardSizeButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_BoardSizeButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.m_BoardSizeButton.Location = new System.Drawing.Point(43, 39);
            this.m_BoardSizeButton.Name = "boardSizeButton";
            this.m_BoardSizeButton.Size = new System.Drawing.Size(273, 39);
            this.m_BoardSizeButton.TabIndex = 0;
            this.m_BoardSizeButton.Text = "Board size:6x6 (click to increase)";
            this.m_BoardSizeButton.UseVisualStyleBackColor = false;
            this.m_BoardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            this.m_BoardSizeButton.MouseLeave += new System.EventHandler(this.boardSizeButton_MouseLeave);
            this.m_BoardSizeButton.MouseHover += new System.EventHandler(this.boardSizeButton_MouseHover);
            this.m_PlayerVsComputerButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_PlayerVsComputerButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.m_PlayerVsComputerButton.Location = new System.Drawing.Point(53, 84);
            this.m_PlayerVsComputerButton.Name = "playerVsComputerButton";
            this.m_PlayerVsComputerButton.Size = new System.Drawing.Size(126, 49);
            this.m_PlayerVsComputerButton.TabIndex = 1;
            this.m_PlayerVsComputerButton.Text = "Play againts the Computer";
            this.m_PlayerVsComputerButton.UseVisualStyleBackColor = false;
            this.m_PlayerVsComputerButton.Click += new System.EventHandler(this.playerVsComputerButton_Click);
            this.m_PlayerVsComputerButton.MouseLeave += new System.EventHandler(this.playerVsComputerButton_MouseLeave);
            this.m_PlayerVsComputerButton.MouseHover += new System.EventHandler(this.playerVsComputerButton_MouseHover);
            this.m_PlayerVsPlayerButton.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.m_PlayerVsPlayerButton.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.m_PlayerVsPlayerButton.Location = new System.Drawing.Point(185, 84);
            this.m_PlayerVsPlayerButton.Name = "PlayerVsPlayerButton";
            this.m_PlayerVsPlayerButton.Size = new System.Drawing.Size(120, 49);
            this.m_PlayerVsPlayerButton.TabIndex = 2;
            this.m_PlayerVsPlayerButton.Text = "Play againts your friend";
            this.m_PlayerVsPlayerButton.UseVisualStyleBackColor = false;
            this.m_PlayerVsPlayerButton.Click += new System.EventHandler(this.playerVsPlayerButton_Click);
            this.m_PlayerVsPlayerButton.MouseLeave += new System.EventHandler(this.playerVsPlayerButton_MouseLeave);
            this.m_PlayerVsPlayerButton.MouseHover += new System.EventHandler(this.PlayerVsPlayerButton_MouseHover);  
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(353, 166);
            this.Controls.Add(this.m_PlayerVsPlayerButton);
            this.Controls.Add(this.m_PlayerVsComputerButton);
            this.Controls.Add(this.m_BoardSizeButton);
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

        private void boardSizeButton_Click(object i_Sender, EventArgs i_EventArgs)
        {
           if (this.m_BoardSize == 12)
            {
                this.m_BoardSize = 6;
            }
           else
            {
                this.m_BoardSize += 2;
            }

            this.m_BoardSizeButton.Text = string.Format("Board size:{0}x{0} (click to increase)", this.m_BoardSize);
        }

        private void playerVsPlayerButton_Click(object i_i_Sender, EventArgs i_EventArgs)
        {
            GameController.BoardSize = this.m_BoardSize;
            GameController.FirstPlayerName = "CoinRed";
            GameController.SecondPlayerName = "CoinYellow";
            GameController.GameType = (int)eGameMenu.PlayerVsPlayer;
            this.Hide();
            this.Close();
            this.m_GameForm = new GameForm();
            this.m_GameForm.ShowDialog();
        }

        private void playerVsComputerButton_Click(object i_Sender, EventArgs i_EventArgs)
        {
            GameController.BoardSize = this.m_BoardSize;
            GameController.FirstPlayerName = "CoinRed";
            GameController.SecondPlayerName = "Computer";
            GameController.GameType = (int)eGameMenu.PlayerVsComputer;
            this.Hide();
            this.Close();
            this.m_GameForm = new GameForm();
            this.m_GameForm.ShowDialog();
        }

        private void boardSizeButton_MouseHover(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_BoardSizeButton.BackColor = Color.CadetBlue;
        }

        private void boardSizeButton_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_BoardSizeButton.BackColor = Color.DarkSeaGreen;
        }

        private void PlayerVsPlayerButton_MouseHover(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_PlayerVsPlayerButton.BackColor = Color.CadetBlue;
        }

        private void playerVsPlayerButton_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_PlayerVsPlayerButton.BackColor = Color.DarkSeaGreen;
        }

        private void playerVsComputerButton_MouseHover(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_PlayerVsComputerButton.BackColor = Color.CadetBlue;
        }

        private void playerVsComputerButton_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            this.m_PlayerVsComputerButton.BackColor = Color.DarkSeaGreen;
        }
    }
}
