namespace TicTacToe
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.btnRandomMove = new System.Windows.Forms.Button();
            this.btnMachineLearningMove = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnPlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRandomMove
            // 
            this.btnRandomMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRandomMove.Location = new System.Drawing.Point(482, 155);
            this.btnRandomMove.Name = "btnRandomMove";
            this.btnRandomMove.Size = new System.Drawing.Size(165, 40);
            this.btnRandomMove.TabIndex = 3;
            this.btnRandomMove.Text = "Random Move";
            this.btnRandomMove.UseVisualStyleBackColor = true;
            this.btnRandomMove.Click += new System.EventHandler(this.btnAIMove_Click);
            // 
            // btnMachineLearningMove
            // 
            this.btnMachineLearningMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMachineLearningMove.Location = new System.Drawing.Point(481, 105);
            this.btnMachineLearningMove.Margin = new System.Windows.Forms.Padding(2);
            this.btnMachineLearningMove.Name = "btnMachineLearningMove";
            this.btnMachineLearningMove.Size = new System.Drawing.Size(170, 41);
            this.btnMachineLearningMove.TabIndex = 4;
            this.btnMachineLearningMove.Text = "MachineLearning Powered Move";
            this.btnMachineLearningMove.UseVisualStyleBackColor = true;
            this.btnMachineLearningMove.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.CausesValidation = false;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(209, 9);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(32, 32);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnHighScores
            // 
            this.btnHighScores.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHighScores.Image = ((System.Drawing.Image)(resources.GetObject("btnHighScores.Image")));
            this.btnHighScores.Location = new System.Drawing.Point(500, 49);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(50, 51);
            this.btnHighScores.TabIndex = 7;
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // btnPlayers
            // 
            this.btnPlayers.FlatAppearance.BorderSize = 0;
            this.btnPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlayers.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayers.Image")));
            this.btnPlayers.Location = new System.Drawing.Point(549, 49);
            this.btnPlayers.Name = "btnPlayers";
            this.btnPlayers.Size = new System.Drawing.Size(101, 51);
            this.btnPlayers.TabIndex = 8;
            this.btnPlayers.UseVisualStyleBackColor = true;
            this.btnPlayers.Click += new System.EventHandler(this.btnPlayers_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(662, 257);
            this.Controls.Add(this.btnPlayers);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnMachineLearningMove);
            this.Controls.Add(this.btnRandomMove);
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A Game of Tic Tac Toe";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRandomMove;
        private System.Windows.Forms.Button btnMachineLearningMove;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnPlayers;
    }
}