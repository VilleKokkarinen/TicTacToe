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

namespace TicTacToe
{
    public partial class Game : Form
    {
        private Player[] Players = { Player.CreateDefaultPlayer(), Player.CreateDefaultPlayer() };
        private int PlayerTurn = 0;
        private Drawing drawing = new Drawing();

        private int moveCount;
        Tile[,] Gameboard = GameBoard.ReturnBoard();
        
      
        private void AI_MOVE()
        {
            Random r = new Random();
            List<Tile> legalMoves = new List<Tile>();
               foreach(Tile tile in Gameboard)
               {
                    if (tile.Value == Tile.TileValue.empty)
                    {
                        legalMoves.Add(tile);
                    }
               }

            Tile randomTile = legalMoves[r.Next(legalMoves.Count - 1)];
                if (PlayerTurn == 0)
                {
                    drawing.DrawCross(randomTile.Panel);
                    SetBoardState(randomTile.X, randomTile.Y, Players[PlayerTurn].PlayerTile);
                    PlayerTurn = 1;
                }
                else
                {
                    drawing.DrawCircle(randomTile.Panel);
                    SetBoardState(randomTile.X, randomTile.Y, Players[PlayerTurn].PlayerTile);
                    PlayerTurn = 0;
                }
        }

        private void Pnl_Click(object sender, EventArgs e)
        {
            Logic((Panel)sender);
        }

        public void Logic(Panel panel)
        {
            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;

            Tile t = Gameboard[x, y];
            if (t.CheckTileState())
            {
                if (PlayerTurn == 0)
                {
                    drawing.DrawCross(panel);
                    SetBoardState(x, y, Players[PlayerTurn].PlayerTile);
                    PlayerTurn = 1;
                }
                else
                {            
                    drawing.DrawCircle(panel);
                    SetBoardState(x, y, Players[PlayerTurn].PlayerTile);
                    PlayerTurn = 0;
                }
            }
        }

        public void ResetGame()
        {
            Gameboard = GameBoard.ReturnBoard();
            moveCount = 0;
            PlayerTurn = 0;
           
            foreach (Tile tile in Gameboard)
            {
                tile.Value = Tile.TileValue.empty;
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
        public void ShowWinner(string Winner)
        {

        }
        public void SetBoardState(int x, int y, Tile.TileValue value)
        {
            bool Winner = false;
            moveCount++;
            int n = 3;
            if(Gameboard[x,y].Value == Tile.TileValue.empty)
            {
                Gameboard[x, y].Value = value;
            }
            //Game Win-conditions

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
                // MessageBox.Show("Draw, no winner");
                Players[0].SaveGame("-", Gameboard);
                ResetGame();
            }
            if(Winner == true)
            {
                if (value == Tile.TileValue.X)
                    Players[0].SaveGame("X", Gameboard);
                else
                    Players[0].SaveGame("O", Gameboard);

               // MessageBox.Show("winner:" + Enum.GetName(typeof(Tile.TileValue), value));
                ResetGame();
            }
        }

        private void btnAIMove_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 25000; i ++)
            AI_MOVE();
        }
    }
}
