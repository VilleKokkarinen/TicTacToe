using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GameLogic: INotifyPropertyChanged
    {
        public event EventHandler<MessageEventArgs> OnMessage;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private Player _Winner;
        public Player Winner
        {
            get { return _Winner; }
            private set
            {
                _Winner = value;
                OnPropertyChanged("Winner");
                OnPropertyChanged("Winner");
            }
        }
        private void RaiseMessage(string message)
        {
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message));
            }
        }

        private void AddWinner(Player winner)
        {
            Winner = winner;
        }

        public bool Reset { get; set; }
        public Player[] Players { get; set; }
        public void CreateDefaultPlayers()
        {
            Players = (new Player[] { Player.CreateDefaultPlayer(), Player.CreateDefaultPlayer() });
        }
        public int PlayerTurn { get; set; }
        public void SwitchPlayerTurn()
        {
            PlayerTurn = (PlayerTurn == 0) ? 1 : 0;
        }

        private Drawing drawing = new Drawing();
        private int moveCount { get; set; }
        public Tile[,] Gameboard { get; set; }
        public void CreateDefaultGame()
        {
            Gameboard = GameBoard.ReturnBoard();
            Reset = false;
            CreateDefaultPlayers();
            PlayerTurn = 0;
            Players[0].PlayerTile = Tile.TileValue.X;
            Players[1].PlayerTile = Tile.TileValue.O;
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
                // MessageBox.Show("Draw, no winner");
                Players[0].SaveGame("DRAW", Gameboard);
                ResetGame();
            }
            if (Winner == true)
            {
                if (value == Tile.TileValue.X)
                    Players[0].SaveGame("X", Gameboard);
                else
                    Players[0].SaveGame("O", Gameboard);

                AddWinner(Players.FirstOrDefault(X => X.PlayerTile == value));
                RaiseMessage("Player " + this.Winner.PlayerTile + " wins");
                ResetGame();
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
            Reset = true;
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
        public void RANDOM_MOVE()
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

            int x = randomTile.Panel.Location.X / 50;
            int y = randomTile.Panel.Location.Y / 50;
            Tile t = Gameboard[x, y];
            if (t.CheckTileState())
            {
                MakeMove(x, y, Players[PlayerTurn].PlayerTile);
            }
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
                    MakeMove(MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                }
            }
            else if (opponentwinningmove)
            {
                if (Gameboard[MoveX, MoveY].CheckTileState())
                {
                    Players[0].SaveMove(Players[PlayerTurn].PlayerTile.ToString(), Gameboard[MoveX, MoveY].ID, Gameboard);
                    MakeMove(MoveX, MoveY, Players[PlayerTurn].PlayerTile);
                }
            }

        }
        public void MakeMove(int X, int Y, Tile.TileValue value)
        {
            // Check if tile is actually empty <-->
            if (PlayerTurn == 0)
            {
                drawing.DrawCross(Gameboard[X, Y].Panel);
            }
            else
            {
                drawing.DrawCircle(Gameboard[X, Y].Panel);
            }

            SetBoardState(X, Y, value);
            SwitchPlayerTurn();
            //PredictMove();           
        }
    }
}
