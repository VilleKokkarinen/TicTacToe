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
            //PredictMove();           
        }

        private void MachineLearningMove()
        {
            string tileplayed = (PlayerTurn == 1) ? "X" : "O";

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
            input.TilePlayed = tileplayed;

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Gameboard)
            {
                legalMoves.Add(tile);
            }
            int ID = Convert.ToInt32(result.Prediction);
            Tile MoveTile = legalMoves.Find(X => X.ID == ID);
            Panel panel = MoveTile.Panel;

            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;
            Tile t = Gameboard[x, y];
            if (t.CheckTileState())
            {
                MakeMove(panel, x, y, Players[PlayerTurn].PlayerTile);
            }
            else
            {
                // MessageBox.Show("Tile was taken, using random move");
                RANDOM_MOVE();
            }

        }
        private void RANDOM_MOVE()
        {
            Random r = new Random();
            List<Tile> legalMoves = new List<Tile>();
            foreach (Tile tile in Gameboard)
            {
                if (tile.Value == Tile.TileValue.NaN)
                {
                    legalMoves.Add(tile);
                }

            }
            Tile randomTile = legalMoves[r.Next(0, legalMoves.Count - 1)];
            Panel panel = randomTile.Panel;

            int x = panel.Location.X / 50;
            int y = panel.Location.Y / 50;
            Tile t = Gameboard[x, y];
            if (t.CheckTileState())
            {
                // Players[0].SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard[x, y].ID, Gameboard);
                MakeMove(panel, x, y, Players[PlayerTurn].PlayerTile);
            }
        }
        
            private bool CheckWinningMove(int x, int y, Tile.TileValue value)
            {
                bool Winner = false;
                int n = 3;

                Tile[,] board = Gameboard;
                //check column

                if (board[x, y].Value == Tile.TileValue.NaN)
                {
                    board[x, y].Value = value;
                }

                for (int i = 0; i < n; i++)
                {
                    if (board[x, i].Value != value)
                        break;
                    if (i == n - 1)
                    {
                        Winner = true;
                    }
                }
                //check row
                for (int i = 0; i < n; i++)
                {
                    if (board[i, y].Value != value)
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
                        if (board[i, i].Value != value)
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
                        if (board[i, (n - 1) - i].Value != value)
                            break;
                        if (i == n - 1)
                        {
                            Winner = true;
                        }
                    }
                }
                //revert changes
                board[x, y].Value = Tile.TileValue.NaN;
                return Winner;
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

                for (int y = 0; y < Gameboard.GetLength(0); y++)
                {
                    for (int x = 0; x < Gameboard.GetLength(1); x++)
                    {
                        Panel p = Gameboard[x, y].Panel;
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
                AddPanels();
                Players[0].PlayerTile = Tile.TileValue.X;
                Players[1].PlayerTile = Tile.TileValue.O;
            }
            public void PredictMove()
            {
                List<Tile> legalMoves = new List<Tile>();
                foreach (Tile tile in Gameboard)
                {
                    if (tile.Value == Tile.TileValue.NaN)
                    {
                        legalMoves.Add(tile);
                    }
                }

                bool winningmove = false;
                bool opponentwinningmove = false;
                int MoveX = 0;
                int MoveY = 0;

                // Executes a winning move if one exists, otherwise executes a move that doesn't allow the opponent to win (unless opponent has a position with 2 or more winning tiles)
                for (int i = 0; i < legalMoves.Count; i++)
                {
                    winningmove = CheckWinningMove(legalMoves[i].X, legalMoves[i].Y, Players[PlayerTurn].PlayerTile);
                    opponentwinningmove = CheckWinningMove(legalMoves[i].X, legalMoves[i].Y, Players[PlayerTurn == 1 ? 0 : 1].PlayerTile);
                    if (winningmove)
                    {
                        MoveX = legalMoves[i].X;
                        MoveY = legalMoves[i].Y;
                        //MessageBox.Show("Winning Tile" + " for " + Players[PlayerTurn].PlayerTile +" is: " + legalMoves[i].X.ToString() + "," + legalMoves[i].Y.ToString());
                        break;
                    }
                    if (opponentwinningmove)
                    {
                        MoveX = legalMoves[i].X;
                        MoveY = legalMoves[i].Y;
                        //MessageBox.Show("Winning Tile" + " for " + Players[PlayerTurn].PlayerTile +" is: " + legalMoves[i].X.ToString() + "," + legalMoves[i].Y.ToString());
                        break;
                    }
                }
                if (winningmove)
                {
                    if (Gameboard[MoveX, MoveY].CheckTileState())
                    {
                        Players[0].SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard[MoveX, MoveY].ID, Gameboard);
                        MakeMove(Gameboard[MoveX, MoveY].Panel, MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                    }
                }
                else if (opponentwinningmove)
                {
                    if (Gameboard[MoveX, MoveY].CheckTileState())
                    {
                        Players[0].SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard[MoveX, MoveY].ID, Gameboard);
                        MakeMove(Gameboard[MoveX, MoveY].Panel, MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                    }
            }

            }
            public void SetBoardState(int x, int y, Tile.TileValue value)
            {
                bool Winner = false;
                int n = 3;
                moveCount++;

                //Players[0].SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard[x, y].ID, Gameboard);

                if (Gameboard[x, y].Value == Tile.TileValue.NaN)
                {
                    Gameboard[x, y].Value = value;
                }
                //Game Win-conditions


                //check column
                for (int i = 0; i < n; i++)
                {
                    if (Gameboard[x, i].Value != value)
                        break;
                    if (i == n - 1)
                    {
                        Winner = true;
                    }
                }
                //check row
                for (int i = 0; i < n; i++)
                {
                    if (Gameboard[i, y].Value != value)
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
                        if (Gameboard[i, (n - 1) - i].Value != value)
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
                if (Winner == true)
                {
                    if (value == Tile.TileValue.X)
                        Players[0].SaveGame("X", Gameboard);
                    else
                        Players[0].SaveGame("O", Gameboard);

                    MessageBox.Show("winner:" + Enum.GetName(typeof(Tile.TileValue), value));
                    ResetGame();
                }
            }            

            private void btnAIMove_Click(object sender, EventArgs e)
            {
            for( int x = 0; x <= 10000; x++ )
                {
                    for (int i = 0; i < 10; i++)
                        PredictMove();

                    RANDOM_MOVE();
                }
              
            }

            private void button1_Click(object sender, EventArgs e)
            {
                //PredictMove();
                MachineLearningMove();
            }
        }
    }