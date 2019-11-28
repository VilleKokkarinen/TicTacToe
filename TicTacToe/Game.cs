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
    public partial class Game : Form
    {
        private GameLogic _Game = new GameLogic();
       
        private void MachineLearningMove()
        {
            string tileplayed = (_Game.PlayerTurn == 1) ? "X" : "O";

            // Add input data
            var input = new ModelInput();
            input.Tile1 = _Game.Gameboard[0, 0].Value.ToString();
            input.Tile2 = _Game.Gameboard[1, 0].Value.ToString();
            input.Tile3 = _Game.Gameboard[2, 0].Value.ToString();
            input.Tile4 = _Game.Gameboard[0, 1].Value.ToString();
            input.Tile5 = _Game.Gameboard[1, 1].Value.ToString();
            input.Tile6 = _Game.Gameboard[2, 1].Value.ToString();
            input.Tile7 = _Game.Gameboard[0, 2].Value.ToString();
            input.Tile8 = _Game.Gameboard[1, 2].Value.ToString();
            input.Tile9 = _Game.Gameboard[2, 2].Value.ToString();
            input.TilePlayed = tileplayed;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in _Game.Gameboard)
            {
                legalMoves.Add(tile);
            }
            int ID = Convert.ToInt32(result.Prediction);
            Tile MoveTile = legalMoves.Find(X => X.ID == ID);
            Panel panel = MoveTile.Panel;

            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;
            Tile t = _Game.Gameboard[x, y];
            if (t.CheckTileState())
            {
                _Game.MakeMove( x, y, _Game.Players[_Game.PlayerTurn].PlayerTile);
            }
            else
            {
                // MessageBox.Show("Tile was taken, using random move");
                _Game.RANDOM_MOVE();
            }

        }
       
        
            private void Pnl_Click(object sender, EventArgs e)
            {
                Panel panel = (Panel)sender;

                int x = panel.Location.X / 50;
                int y = panel.Location.Y / 50;

                Tile t = _Game.Gameboard[x, y];
                if (t.CheckTileState())
                {
                    lblWinner.Text = "";
                    _Game.MakeMove(x, y, _Game.Players[_Game.PlayerTurn].PlayerTile);

                if (_Game.Reset == true)
                {
                    lblWinner.Text = "Winner: " + _Game.Winner.PlayerTile.ToString();
                    _Game.Reset = false;
                    Refresh();
                }
                }
            }
         
            public void AddPanels()
            {
                Point newLoc = new Point(25, 25);
                int offset = 25 + 50 + 1;

                for (int y = 0; y < _Game.Gameboard.GetLength(0); y++)
                {
                    for (int x = 0; x < _Game.Gameboard.GetLength(1); x++)
                    {
                        Panel p = _Game.Gameboard[x, y].Panel;
                        p.Size = new Size(50, 50);
                        p.Location = newLoc;
                        p.Click += Pnl_Click;
                        newLoc.Offset(55, 0);
                        Controls.Add(p);
                    }
                    newLoc.Offset(-165, 55);
                }
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Panel HorizontalLine = new Panel
                        {
                            Size = new Size(180, 3),
                            Location = new Point(15, 55 * j + offset),
                            BackColor = Color.Black
                        };
                        Controls.Add(HorizontalLine);
                    }
                    Panel VerticalLine = new Panel
                    {
                        Size = new Size(3, 180),
                        Location = new Point(55 * i + offset, 15),
                        BackColor = Color.Black
                    };
                    Controls.Add(VerticalLine);
                }
            }

            public Game()
            {
                InitializeComponent();
                _Game.CreateDefaultGame();
                AddPanels();
            }
           
             

            private void btnAIMove_Click(object sender, EventArgs e)
            {
                for( int x = 0; x <= 10000; x++ ) {
                    for (int i = 0; i < 10; i++)
                        _Game.PredictMove();
                    _Game.RANDOM_MOVE();
                }              
            }

            private void button1_Click(object sender, EventArgs e)
            {
                //PredictMove();
                MachineLearningMove();
            }
        }
    }