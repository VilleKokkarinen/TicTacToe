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
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.btnAIMove = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(357, 15);
            this.lblPlayerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(89, 17);
            this.lblPlayerName.TabIndex = 1;
            this.lblPlayerName.Text = "Player Name";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(361, 36);
            this.txtPlayerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(132, 22);
            this.txtPlayerName.TabIndex = 2;
            // 
            // btnAIMove
            // 
            this.btnAIMove.Location = new System.Drawing.Point(361, 89);
            this.btnAIMove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAIMove.Name = "btnAIMove";
            this.btnAIMove.Size = new System.Drawing.Size(100, 28);
            this.btnAIMove.TabIndex = 3;
            this.btnAIMove.Text = "Make AI Move";
            this.btnAIMove.UseVisualStyleBackColor = true;
            this.btnAIMove.Click += new System.EventHandler(this.btnAIMove_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(416, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Predict a Good Move";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 394);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAIMove);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.lblPlayerName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Game";
            this.Text = "A Game of Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnAIMove;
        private System.Windows.Forms.Button button1;
    }
}