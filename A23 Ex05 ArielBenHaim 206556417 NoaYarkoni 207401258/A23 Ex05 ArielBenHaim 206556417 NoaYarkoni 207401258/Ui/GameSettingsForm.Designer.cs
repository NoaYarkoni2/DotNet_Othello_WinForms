namespace Ui
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonOnePlayer = new System.Windows.Forms.Button();
            this.buttonTwoPlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(61, 24);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(549, 102);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board size: 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonOnePlayer
            // 
            this.buttonOnePlayer.Location = new System.Drawing.Point(61, 143);
            this.buttonOnePlayer.Name = "buttonOnePlayer";
            this.buttonOnePlayer.Size = new System.Drawing.Size(262, 82);
            this.buttonOnePlayer.TabIndex = 1;
            this.buttonOnePlayer.Text = "Play against the computer";
            this.buttonOnePlayer.UseVisualStyleBackColor = true;
            this.buttonOnePlayer.Click += new System.EventHandler(this.buttonOnePlayer_Click);
            // 
            // buttonTwoPlayers
            // 
            this.buttonTwoPlayers.Location = new System.Drawing.Point(348, 143);
            this.buttonTwoPlayers.Name = "buttonTwoPlayers";
            this.buttonTwoPlayers.Size = new System.Drawing.Size(262, 82);
            this.buttonTwoPlayers.TabIndex = 2;
            this.buttonTwoPlayers.Text = "Play against your friend";
            this.buttonTwoPlayers.UseVisualStyleBackColor = true;
            this.buttonTwoPlayers.Click += new System.EventHandler(this.buttonTwoPlayers_Click);
            // 
            // GameSettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(646, 258);
            this.Controls.Add(this.buttonTwoPlayers);
            this.Controls.Add(this.buttonOnePlayer);
            this.Controls.Add(this.buttonBoardSize);
            this.Name = "GameSettingsForm";
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.GameSettingsForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonIncrease;
        private System.Windows.Forms.Button buttonPlayAgainstComputer;
        private System.Windows.Forms.Button buttonPlayAgaistFriend;
        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonOnePlayer;
        private System.Windows.Forms.Button buttonTwoPlayers;
    }
}

