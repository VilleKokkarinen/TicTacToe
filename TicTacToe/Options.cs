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
    public partial class Options : Form
    {
        GameLogic game;
        public Options(ref GameLogic game)
        {
            InitializeComponent();
            //Set the focus to a label, so its practicly not selecting anything at start
            ActiveControl = label1;
            this.game = game;
            txtPlayerNameX.Text = game.Players[0].Name;
            txtPlayerNameO.Text = game.Players[1].Name;
            chkThinkTime.Checked = game.Delays;
            chkAutomaticWinLoss.Checked = game.AutomaticWinLose;
        }

        void SaveData()
        {
            game.Players[0].Name = txtPlayerNameX.Text;
            game.Players[1].Name = txtPlayerNameO.Text;
            game.Delays = chkThinkTime.Checked;
            game.AutomaticWinLose = chkAutomaticWinLoss.Checked;

            game.SaveOptions(game.Delays, game.AutomaticWinLose, game.Players);
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
    }
}
