namespace TicTacToe
{
    partial class HighScores
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
            this.dgTopTen = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgTopTen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTopTen
            // 
            this.dgTopTen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTopTen.Location = new System.Drawing.Point(12, 12);
            this.dgTopTen.Name = "dgTopTen";
            this.dgTopTen.Size = new System.Drawing.Size(575, 281);
            this.dgTopTen.TabIndex = 0;
            // 
            // HighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 305);
            this.Controls.Add(this.dgTopTen);
            this.Name = "HighScores";
            this.Text = "HighScores";
            ((System.ComponentModel.ISupportInitialize)(this.dgTopTen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTopTen;
    }
}