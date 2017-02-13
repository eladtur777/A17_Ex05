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
        private int boardSize = 6;
        GameModel m_Logic;

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
            this.boardSizeButton.Location = new System.Drawing.Point(63, 39);
            this.boardSizeButton.Name = "boardSizeButton";
            this.boardSizeButton.Size = new System.Drawing.Size(252, 39);
            this.boardSizeButton.TabIndex = 0;
            this.boardSizeButton.Text = "Board size:6x6 (click to increase)";
            this.boardSizeButton.UseVisualStyleBackColor = true;
            this.boardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            // 
            // playerVsComputerButton
            // 
            this.playerVsComputerButton.Location = new System.Drawing.Point(63, 84);
            this.playerVsComputerButton.Name = "playerVsComputerButton";
            this.playerVsComputerButton.Size = new System.Drawing.Size(116, 49);
            this.playerVsComputerButton.TabIndex = 1;
            this.playerVsComputerButton.Text = "Play againts the Computer";
            this.playerVsComputerButton.UseVisualStyleBackColor = true;
            this.playerVsComputerButton.Click += new System.EventHandler(this.playerVsComputerButton_Click);
            // 
            // PlayerVsPlayerButton
            // 
            this.PlayerVsPlayerButton.Location = new System.Drawing.Point(195, 84);
            this.PlayerVsPlayerButton.Name = "PlayerVsPlayerButton";
            this.PlayerVsPlayerButton.Size = new System.Drawing.Size(120, 49);
            this.PlayerVsPlayerButton.TabIndex = 2;
            this.PlayerVsPlayerButton.Text = "Play againts your friend";
            this.PlayerVsPlayerButton.UseVisualStyleBackColor = true;
            this.PlayerVsPlayerButton.Click += new System.EventHandler(this.PlayerVsPlayerButton_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(360, 166);
            this.Controls.Add(this.PlayerVsPlayerButton);
            this.Controls.Add(this.playerVsComputerButton);
            this.Controls.Add(this.boardSizeButton);
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
           if(boardSize == 12)
            {
                boardSize = 6;
            }
           else
            {
                boardSize += 2;
            }
            this.boardSizeButton.Text = string.Format("Board size:{0}x{0} (click to increase)", boardSize);
        }

        private void PlayerVsPlayerButton_Click(object sender, EventArgs e)
        {
            m_Logic = new GameModel(boardSize,"Yellow","Red");
            this.Hide();
            this.Close();
            Game g = new Game();
            g.ShowDialog();
        }

        private void playerVsComputerButton_Click(object sender, EventArgs e)
        {
            m_Logic = new GameModel(boardSize, "Yellow", "Computer");
            this.Hide();
            this.Close();
            Game g = new Game();
            g.ShowDialog();
        }
    }
}
