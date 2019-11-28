using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Engine;
using TicTacToeML.Model;

namespace TicTacToe
{
    public partial class GameForm : Form
    {
        private GameLogic Game = new GameLogic();
        bool checking = false;
        public GameForm()
        {
            InitializeComponent();
            Game.CreateDefaultGame();
            AddPanels();
        }
        private void MachineLearningMove()
        {
            string tileplayed = (Game.PlayerTurn == 1) ? "X" : "O";

            // Add input data
            var input = new ModelInput();
            input.Tile1 = Game.Gameboard[0, 0].Value.ToString();
            input.Tile2 = Game.Gameboard[1, 0].Value.ToString();
            input.Tile3 = Game.Gameboard[2, 0].Value.ToString();
            input.Tile4 = Game.Gameboard[0, 1].Value.ToString();
            input.Tile5 = Game.Gameboard[1, 1].Value.ToString();
            input.Tile6 = Game.Gameboard[2, 1].Value.ToString();
            input.Tile7 = Game.Gameboard[0, 2].Value.ToString();
            input.Tile8 = Game.Gameboard[1, 2].Value.ToString();
            input.Tile9 = Game.Gameboard[2, 2].Value.ToString();
            input.TilePlayed = tileplayed;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Game.Gameboard)
            {
                legalMoves.Add(tile);
            }
            int ID = Convert.ToInt32(result.Prediction);
            Tile MoveTile = legalMoves.Find(X => X.ID == ID);
            Panel panel = MoveTile.Panel;

            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;
            Tile t = Game.Gameboard[x, y];
            if (t.CheckTileState())
            {
                Game.MakeMove( x, y, Game.Players[Game.PlayerTurn].PlayerTile);
            }
            else
            {
                // MessageBox.Show("Tile was taken, using random move");
                Game.RANDOM_MOVE();
            }

        }
       
        
        private void Pnl_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            int x = (panel.Location.X - 50) / 50;
            int y = (panel.Location.Y - 50) / 50;

            Tile t = Game.Gameboard[x, y];
            if (t.CheckTileState() && Game.Over == false)
            {
                Game.MakeMove(x, y, Game.Players[Game.PlayerTurn].PlayerTile);              
            }
            CheckGameOver();
        }
         
        public void AddPanels()
        {
        Point newLoc = new Point(50, 50);
        int offset = 100;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Panel HorizontalLine = new Panel
                {
                    Size = new Size(150, 3),
                    Location = new Point(50, 50 * j + offset - 2),
                    BackColor = Color.Black
                };
                Controls.Add(HorizontalLine);
            }
            Panel VerticalLine = new Panel
            {
                Size = new Size(3, 150),
                Location = new Point(50 * i + offset - 2, 50),
                BackColor = Color.Black
            };
            Controls.Add(VerticalLine);
        }
        for (int y = 0; y < Game.Gameboard.GetLength(0); y++)
        {
            for (int x = 0; x < Game.Gameboard.GetLength(1); x++)
            {
                Panel p = Game.Gameboard[x, y].Panel;
                p.Size = new Size(50, 50);
                p.Location = newLoc;
                p.BackColor = Color.Transparent;
                p.Click += Pnl_Click;
                newLoc.Offset(50, 0);
                Controls.Add(p);
            }
            newLoc.Offset(-150, 50);
        }
        }

        private void CheckGameOver()
        {
            if (Game.Over == true && checking == false && Game.Winner != null)
            {
                if(Game.Winner != "Draw")
                    DisplayWinner("Winner: " + Game.Winner);
                else
                {
                    DisplayWinner(Game.Winner);
                }
            }
        }

        private void DisplayWinner(string winner, int Interval = 2500)
        {
            checking = true;
            lblWinner.Text = winner;
            lblWinner.Show();
            var t = new Timer();
            t.Interval = Interval;
            t.Tick += (s, e) =>
            {
                lblWinner.Hide();
                t.Stop();

                Game.ResetGame();
                Refresh();
                checking = false;

                lblWinner.Text = "";
            };
            t.Start();
        }

        private void btnAIMove_Click(object sender, EventArgs e)
        {
            Game.RANDOM_MOVE();
            CheckGameOver();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MachineLearningMove();
            CheckGameOver();
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Options o = new Options(ref Game);
            o.ShowDialog();
        }

        private void btnHiScores_Click(object sender, EventArgs e)
        {

        }
    }
}