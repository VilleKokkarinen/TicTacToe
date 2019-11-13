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
            this.SuspendLayout();
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(268, 12);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(67, 13);
            this.lblPlayerName.TabIndex = 1;
            this.lblPlayerName.Text = "Player Name";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(271, 29);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(100, 20);
            this.txtPlayerName.TabIndex = 2;
            // 
            // btnAIMove
            // 
            this.btnAIMove.Location = new System.Drawing.Point(271, 72);
            this.btnAIMove.Name = "btnAIMove";
            this.btnAIMove.Size = new System.Drawing.Size(75, 23);
            this.btnAIMove.TabIndex = 3;
            this.btnAIMove.Text = "Make AI Move";
            this.btnAIMove.UseVisualStyleBackColor = true;
            this.btnAIMove.Click += new System.EventHandler(this.btnAIMove_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 320);
            this.Controls.Add(this.btnAIMove);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.lblPlayerName);
            this.Name = "Game";
            this.Text = "A Game of Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnAIMove;
    }
}