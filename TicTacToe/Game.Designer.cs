namespace TicTacToe
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.btnRandomMove = new System.Windows.Forms.Button();
            this.btnMachineLearningMove = new System.Windows.Forms.Button();
            this.lblWinner = new System.Windows.Forms.Label();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnHiScores = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.progbarCPU = new System.Windows.Forms.ProgressBar();
            this.lblCPUthink = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRandomMove
            // 
            this.btnRandomMove.Location = new System.Drawing.Point(249, 49);
            this.btnRandomMove.Name = "btnRandomMove";
            this.btnRandomMove.Size = new System.Drawing.Size(101, 51);
            this.btnRandomMove.TabIndex = 3;
            this.btnRandomMove.Text = "Random Move";
            this.toolTip.SetToolTip(this.btnRandomMove, "CPU makes a random move for current player");
            this.btnRandomMove.UseVisualStyleBackColor = true;
            this.btnRandomMove.Click += new System.EventHandler(this.btnRandom_Move_Click);
            // 
            // btnMachineLearningMove
            // 
            this.btnMachineLearningMove.Location = new System.Drawing.Point(249, 99);
            this.btnMachineLearningMove.Margin = new System.Windows.Forms.Padding(2);
            this.btnMachineLearningMove.Name = "btnMachineLearningMove";
            this.btnMachineLearningMove.Size = new System.Drawing.Size(101, 51);
            this.btnMachineLearningMove.TabIndex = 4;
            this.btnMachineLearningMove.Text = "MachineLearning Powered Move";
            this.toolTip.SetToolTip(this.btnMachineLearningMove, "Uses a premade Machine-Learning model to determine a \"good\" move for current play" +
        "er");
            this.btnMachineLearningMove.UseVisualStyleBackColor = true;
            this.btnMachineLearningMove.Click += new System.EventHandler(this.btn_MachineLearningMove_Click);
            // 
            // lblWinner
            // 
            this.lblWinner.AutoSize = true;
            this.lblWinner.BackColor = System.Drawing.Color.Transparent;
            this.lblWinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinner.Location = new System.Drawing.Point(245, 15);
            this.lblWinner.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new System.Drawing.Size(138, 25);
            this.lblWinner.TabIndex = 5;
            this.lblWinner.Text = "                     ";
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(249, 199);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(101, 51);
            this.btnOptions.TabIndex = 6;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnHiScores
            // 
            this.btnHiScores.Location = new System.Drawing.Point(249, 249);
            this.btnHiScores.Name = "btnHiScores";
            this.btnHiScores.Size = new System.Drawing.Size(101, 51);
            this.btnHiScores.TabIndex = 7;
            this.btnHiScores.Text = "HighScores";
            this.btnHiScores.UseVisualStyleBackColor = true;
            this.btnHiScores.Click += new System.EventHandler(this.btnHiScores_Click);
            // 
            // progbarCPU
            // 
            this.progbarCPU.Location = new System.Drawing.Point(152, 22);
            this.progbarCPU.Name = "progbarCPU";
            this.progbarCPU.Size = new System.Drawing.Size(95, 23);
            this.progbarCPU.TabIndex = 8;
            this.progbarCPU.Visible = false;
            // 
            // lblCPUthink
            // 
            this.lblCPUthink.AutoSize = true;
            this.lblCPUthink.Location = new System.Drawing.Point(150, 6);
            this.lblCPUthink.Name = "lblCPUthink";
            this.lblCPUthink.Size = new System.Drawing.Size(69, 13);
            this.lblCPUthink.TabIndex = 9;
            this.lblCPUthink.Text = "CPU thinking";
            this.lblCPUthink.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TicTacToe.Properties.Resources.Tile;
            this.ClientSize = new System.Drawing.Size(572, 471);
            this.Controls.Add(this.lblCPUthink);
            this.Controls.Add(this.progbarCPU);
            this.Controls.Add(this.btnHiScores);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.lblWinner);
            this.Controls.Add(this.btnMachineLearningMove);
            this.Controls.Add(this.btnRandomMove);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A Game of Tic Tac Toe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRandomMove;
        private System.Windows.Forms.Button btnMachineLearningMove;
        private System.Windows.Forms.Label lblWinner;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnHiScores;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ProgressBar progbarCPU;
        private System.Windows.Forms.Label lblCPUthink;
    }
}