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

        private int moveCount;
        private readonly int _LineWidth = new Params().LineWidth;
        private readonly int _Margin = new Params().Margin;
        private readonly int _Size = new Params().Size;
        private Params props = new Params();
        Tile[,] Gameboard = GameBoard.ReturnBoard();
        
        private void Pnl_Click(object sender, EventArgs e)
        {
            Panel pnl = (Panel)sender;
            int x = pnl.Location.X / 50;
            int y = pnl.Location.Y / 50;

            Tile t = Gameboard[x, y];
            if (CheckTileState(t))
            {
                SetBoardState(x, y, Players[PlayerTurn].PlayerTile);
                if (PlayerTurn == 0)
                {
                    DrawCross((Panel)sender);
                    PlayerTurn = 1;
                }
                else
                {
                    DrawCircle((Panel)sender);
                    PlayerTurn = 0;
                }                
            }
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

        public void DrawCircle(Panel panel)
        {
            int x, y, width, height;
            // marginin verran vasemmasta ylänurkasta
            x = _Margin;
            y = _Margin;
            // Ympyrän koosta vähennetään 2 marginin verran. (1 alanurkasta, + 1 koosta)
            width = _Size - _Margin * 2;
            height = _Size - _Margin * 2;

            Graphics g = panel.CreateGraphics();
            Pen _pen = new Pen(Color.Black, _LineWidth);

            g.DrawEllipse(_pen, x, y, width, height);
        }
        public void DrawCross(Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            Pen _pen = new Pen(Color.Black, _LineWidth);
            // viiva \
            g.DrawLine(_pen, new Point(_Margin, _Margin), new Point(_Size - _Margin, _Size - _Margin));
            // viiva /
            g.DrawLine(_pen, new Point(_Margin, _Size - _Margin), new Point(_Size - _Margin, _Margin));

        }

        public Game()
        {
            InitializeComponent();
            AddPanels();
            Players[0].PlayerTile = Tile.TileValue.X;
            Players[1].PlayerTile = Tile.TileValue.O;
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
                    MessageBox.Show("winner: " + Enum.GetName(typeof(Tile.TileValue), value));
                    Winner = true;

                    Player winningPlayer;
                    if (value == Tile.TileValue.X)
                        winningPlayer = Players[0];
                    else
                        winningPlayer = Players[1];

                    SaveGame(Players[0], Players[1], winningPlayer, Gameboard);
                }
            }
            //check row
            for (int i = 0; i < n; i++)
            {
                if (Gameboard[i,y].Value != value)
                    break;
                if (i == n - 1)
                {
                    MessageBox.Show("winner: " + Enum.GetName(typeof(Tile.TileValue), value));
                    Winner = true;

                    Player winningPlayer;
                    if (value == Tile.TileValue.X)
                        winningPlayer = Players[0];
                    else
                        winningPlayer = Players[1];

                    SaveGame(Players[0], Players[1], winningPlayer, Gameboard);
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
                        MessageBox.Show("winner: " + Enum.GetName(typeof(Tile.TileValue), value));
                        Winner = true;

                        Player winningPlayer;
                        if (value == Tile.TileValue.X)
                            winningPlayer = Players[0];
                        else
                            winningPlayer = Players[1];

                        SaveGame(Players[0], Players[1], winningPlayer, Gameboard);
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
                        MessageBox.Show("winner:" + Enum.GetName(typeof(Tile.TileValue), value));
                        Winner = true;

                        Player winningPlayer;
                        if (value == Tile.TileValue.X)
                            winningPlayer = Players[0];
                        else
                            winningPlayer = Players[1];

                        SaveGame(Players[0], Players[1], winningPlayer, Gameboard);
                    }
                }
            }

            //check draw
            if (moveCount == (Math.Pow(n, 2)) && Winner != true)
            {
                MessageBox.Show("Draw, no winner");
            }
        }

        private void SaveGame(Player player1, Player player2, Player Winner, Tile[,]Gameboard)
        {
            File.WriteAllText("GameData.xml", props.ToXmlString(player1.Name,player2.Name,Winner.Name,Gameboard));
        }
        private bool CheckTileState(Tile t)
        {
            if (t.Value == Tile.TileValue.empty)
                return true;
            else
                return false;
        }
    }
}
