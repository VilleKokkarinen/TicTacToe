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
            this.game = game;
        }

        void SaveData()
        {
            game.Players[0].PlayerName = txtPlayerNameX.Text;
            game.Players[1].PlayerName = txtPlayerNameO.Text;
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
