using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    /// <summary>
    /// Options form
    /// </summary>
    public partial class Options : Form
    {
        /// <summary>
        /// referenced copy of the GameLogic
        /// </summary>
        GameLogic game;
        public Options(ref GameLogic game)
        {
            InitializeComponent();
            //Set the focus to a label, so its practicly not selecting anything at start
            ActiveControl = label1;

            // add the reference
            this.game = game;

            // add all the option values to current values
            txtPlayerNameX.Text = game.Players[0].Name;
            txtPlayerNameO.Text = game.Players[1].Name;
            checkCPUX.Checked = game.Players[0].IsCPU;
            checkCPUO.Checked = game.Players[1].IsCPU;
            chkThinkTime.Checked = game.Delays;
            chkAutomaticWinLoss.Checked = game.AutomaticWinLose;
            sliderBoardSize.Value = game.BoardSize;
        }

        /// <summary>
        /// Saves options to file, and resets the board
        /// </summary>
        void SaveData()
        {
            // put all values to game values
            game.BoardSize = sliderBoardSize.Value;
            game.Players[0].Name = txtPlayerNameX.Text;
            game.Players[0].IsCPU = checkCPUX.Checked;
            game.Players[1].IsCPU = checkCPUO.Checked;
            game.Players[1].Name = txtPlayerNameO.Text;
            game.Delays = chkThinkTime.Checked;
            game.AutomaticWinLose = chkAutomaticWinLoss.Checked;

            // save options to file
            game.SaveOptions(game.Delays, game.AutomaticWinLose, game.BoardSize, game.Players);

            // reset the board
            game.NewBoard();
        }
        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void Options_FormClosing(object sender, EventArgs e)
        {
            SaveData();
            Close();
        }

        /// <summary>
        /// Slider for the boardsize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderBoardSize_ValueChanged(object sender, EventArgs e)
        {
            // move the value label of the slider to the location the sliders pointer is at
            int offset = Convert.ToInt32(lblSliderValue.Text[0].ToString());
            offset = sliderBoardSize.Value - offset;
            offset *= 30;
            lblSliderValue.Location = new Point(lblSliderValue.Location.X+offset, lblSliderValue.Location.Y);
            lblSliderValue.Text = sliderBoardSize.Value.ToString() + " - Board-Size";
        }
    }
}
