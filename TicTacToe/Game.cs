﻿using System;
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
        private Player[] Players = { Player.CreateDefaultPlayer(), Player.CreateDefaultPlayer() };
        private int PlayerTurn = 0;
        private Drawing drawing = new Drawing();

        private int moveCount;
        Tile[,] Gameboard = GameBoard.ReturnBoard();
        
        private void MakeMove(Panel p, int X, int Y, Tile.TileValue value)
        {
            if (PlayerTurn == 0)
            {
                drawing.DrawCross(p);
                SetBoardState(X, Y, value);
                PlayerTurn = 1;
            }
            else
            {
                drawing.DrawCircle(p);
                SetBoardState(X, Y, value);
                PlayerTurn = 0;
            }
        }
      
        private string predictwinner()
        {            
            // Add input data
            var input = new ModelInput();
            input.Tile1 = Gameboard[0, 0].Value.ToString();
            input.Tile2 = Gameboard[1, 0].Value.ToString();
            input.Tile3 = Gameboard[2, 0].Value.ToString();
            input.Tile4 = Gameboard[0, 1].Value.ToString();
            input.Tile5 = Gameboard[1, 1].Value.ToString();
            input.Tile6 = Gameboard[2, 1].Value.ToString();
            input.Tile7 = Gameboard[0, 2].Value.ToString();
            input.Tile8 = Gameboard[1, 2].Value.ToString();
            input.Tile9 = Gameboard[2, 2].Value.ToString();

            List<string> onboard = new List<string>();
            List<string> prediction = new List<string>();
            prediction.Add(input.Tile1);
            prediction.Add(input.Tile2);
            prediction.Add(input.Tile3);
            prediction.Add(input.Tile4);
            prediction.Add(input.Tile5);
            prediction.Add(input.Tile6);
            prediction.Add(input.Tile7);
            prediction.Add(input.Tile8);
            prediction.Add(input.Tile9);

            int occupied = 0;
            foreach (Tile tile in Gameboard)
            {
                if (tile.Value != Tile.TileValue.NaN)
                    occupied++;

                onboard.Add(tile.Value.ToString());

            }
            input.OccupiedTiles = occupied;


            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            return result.Prediction;
        }
        private void AI_MOVE()
        {
            Random r = new Random();
            List<Tile> legalMoves = new List<Tile>();
               foreach(Tile tile in Gameboard)
               {
                    if (tile.Value == Tile.TileValue.NaN)
                    {
                        legalMoves.Add(tile);
                    }
               }

            Tile randomTile = legalMoves[r.Next(legalMoves.Count - 1)];

            MakeMove(randomTile.Panel, randomTile.X, randomTile.Y, Players[PlayerTurn].PlayerTile);
        }

        private void Pnl_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;

            Tile t = Gameboard[x, y];
            if (t.CheckTileState())
            {
                MakeMove(panel, x, y, Players[PlayerTurn].PlayerTile);
            }
        }
        public void ResetGame()
        {
            Gameboard = GameBoard.ReturnBoard();
            moveCount = 0;
            PlayerTurn = 0;
           
            foreach (Tile tile in Gameboard)
            {
                tile.Value = Tile.TileValue.NaN;
            }
            Refresh();
        }
        public void AddPanels()
        {
            Point newLoc = new Point(25, 25);
            int offset = 25 + 50 + 1;

            for (int i = 0; i < Gameboard.GetLength(0); i++)
            {
                for(int j = 0; j < Gameboard.GetLength(1); j++)
                {
                    Panel p = Gameboard[i, j].Panel;
                    p.Size = new Size(50, 50);
                    p.Location = newLoc;
                    p.Click += Pnl_Click;
                    newLoc.Offset(55,0);
                    Controls.Add(p);
                }
                newLoc.Offset(-165, 55);
            }
            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
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
            AddPanels();
            Players[0].PlayerTile = Tile.TileValue.X;
            Players[1].PlayerTile = Tile.TileValue.O;
        }
        public void checkwinnerAI()
        {
            if (moveCount >= 5)
            {
                string prediction = predictwinner();
                if (prediction != "E")
                {
                    if (prediction == "X")
                    {
                        MessageBox.Show("X Wins");
                        // Players[0].SaveGame("X", Gameboard);
                        // ResetGame();
                    }
                    else if (prediction == "O")
                    {
                        MessageBox.Show("O Wins");
                        // Players[0].SaveGame("O", Gameboard);
                        // ResetGame();
                    }
                    else if (prediction == "DRAW" && moveCount == 9)
                    {
                        MessageBox.Show("Draw, no winner");
                        // Players[0].SaveGame("D", Gameboard);
                        // ResetGame();
                    }
                }
            }
        }
        public void SetBoardState(int x, int y, Tile.TileValue value)
        {
            bool Winner = false;
            int n = 3;
            moveCount++;

            if (Gameboard[x, y].Value == Tile.TileValue.NaN)
            {
                Gameboard[x, y].Value = value;
            }
            //Game Win-conditions

            //checkwinnerAI();
            
            //check column
            for (int i = 0; i < n; i++)
            {
                if (Gameboard[x,i].Value != value)
                    break;
                if (i == n - 1)
                {
                    Winner = true;
                }
            }
            //check row
            for (int i = 0; i < n; i++)
            {
                if (Gameboard[i,y].Value != value)
                    break;
                if (i == n - 1)
                {
                    Winner = true;
                }
            }

            //check diagonal
            if (x == y)
            {
                for (int i = 0; i < n; i++)
                {
                    if (Gameboard[i, i].Value != value)
                        break;
                    if (i == n - 1)
                    {
                        Winner = true;
                    }
                }
            }

            //check diagonal (the other way)
            if (x + y == n - 1)
            {
                for (int i = 0; i < n; i++)
                {
                    if (Gameboard[i,(n - 1) - i].Value != value)
                        break;
                    if (i == n - 1)
                    {                        
                        Winner = true;
                    }
                }
            }
            
            //check draw
            if (moveCount == (Math.Pow(n, 2)) && Winner != true)
            {
                MessageBox.Show("Draw, no winner");
                Players[0].SaveGame("DRAW", Gameboard);
                ResetGame();
            }
            if(Winner == true)
            {
                if (value == Tile.TileValue.X)
                    Players[0].SaveGame("X", Gameboard);
                else
                    Players[0].SaveGame("O", Gameboard);

                MessageBox.Show("winner:" + Enum.GetName(typeof(Tile.TileValue), value));
                ResetGame();
            }
            else
            {
                if(moveCount >= 5)
                Players[0].SaveGame("E", Gameboard);
            }
            
            
        }

        private void btnAIMove_Click(object sender, EventArgs e)
        {
            //for(int i = 0; i < 1000000; i ++)
                AI_MOVE();
        }
    }
}
