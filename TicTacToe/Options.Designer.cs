namespace TicTacToe
{
    partial class Options
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
            this.txtPlayerNameX = new System.Windows.Forms.TextBox();
            this.txtPlayerNameO = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkThinkTime = new System.Windows.Forms.CheckBox();
            this.chkAutomaticWinLoss = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // txtPlayerNameX
            // 
            this.txtPlayerNameX.Location = new System.Drawing.Point(12, 51);
            this.txtPlayerNameX.Name = "txtPlayerNameX";
            this.txtPlayerNameX.Size = new System.Drawing.Size(100, 20);
            this.txtPlayerNameX.TabIndex = 1;
            // 
            // txtPlayerNameO
            // 
            this.txtPlayerNameO.Location = new System.Drawing.Point(248, 50);
            this.txtPlayerNameO.Name = "txtPlayerNameO";
            this.txtPlayerNameO.Size = new System.Drawing.Size(100, 20);
            this.txtPlayerNameO.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Name for X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Player Name for O";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "< Go back <";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Options_FormClosing);
            // 
            // chkThinkTime
            // 
            this.chkThinkTime.AutoSize = true;
            this.chkThinkTime.Checked = true;
            this.chkThinkTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThinkTime.Location = new System.Drawing.Point(16, 223);
            this.chkThinkTime.Name = "chkThinkTime";
            this.chkThinkTime.Size = new System.Drawing.Size(58, 17);
            this.chkThinkTime.TabIndex = 4;
            this.chkThinkTime.Text = "Delays";
            this.toolTip.SetToolTip(this.chkThinkTime, "Animation Times, CPU thinking time etc...");
            this.chkThinkTime.UseVisualStyleBackColor = true;
            // 
            // chkAutomaticWinLoss
            // 
            this.chkAutomaticWinLoss.AutoSize = true;
            this.chkAutomaticWinLoss.Location = new System.Drawing.Point(16, 246);
            this.chkAutomaticWinLoss.Name = "chkAutomaticWinLoss";
            this.chkAutomaticWinLoss.Size = new System.Drawing.Size(194, 17);
            this.chkAutomaticWinLoss.TabIndex = 5;
            this.chkAutomaticWinLoss.Text = "Automatic- Winner / Loss preventer";
            this.toolTip.SetToolTip(this.chkAutomaticWinLoss, "Enables automatic win condition, when there are 2 of same Tile on the gameboard, " +
        "on a singe line");
            this.chkAutomaticWinLoss.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 366);
            this.Controls.Add(this.chkAutomaticWinLoss);
            this.Controls.Add(this.chkThinkTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPlayerNameO);
            this.Controls.Add(this.txtPlayerNameX);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlayerNameX;
        private System.Windows.Forms.TextBox txtPlayerNameO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkThinkTime;
        private System.Windows.Forms.CheckBox chkAutomaticWinLoss;
        private System.Windows.Forms.ToolTip toolTip;
    }
}