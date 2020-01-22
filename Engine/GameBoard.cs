using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// The 2D array of Tiles / or the gameboard
    /// </summary>
    public class GameBoard
    {
        private Tile[,] Board;

        /// <summary>
        /// Create new gameboard of N size
        /// </summary>
        /// <param name="Size"></param>
        public GameBoard(int Size = 3)
        {
            Board = new Tile[Size, Size];
            AddTilesToBoard(Size);
        }

        /// <summary>
        /// Adds the tiles to the gameboard
        /// </summary>
        /// <param name="Size"></param>
        private void AddTilesToBoard(int Size = 3)
        {
           // Each tile gets a ID
           int ID = 0;

           for(int X = 0; X < Size; X++)
           {
                for(int Y = 0; Y < Size; Y++)
                {
                    // Add a new empty tile to the board
                    Board[X, Y] = new Tile(ID, X, Y, Tile.TileValue.NaN, new System.Windows.Forms.Panel());
                    ID++;
                }
           }
        }

        /// <summary>
        /// Returns the current board
        /// </summary>
        /// <returns></returns>
        public Tile[,] getboard()
        {
            return Board;
        }

        /// <summary>
        /// Set the tile value at [X, Y] to given value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetBoardState(int x, int y, Tile.TileValue value)
        {
            // check if empty, add value if empty
            if (Board[x, y].Value == Tile.TileValue.NaN)
            {
                Board[x, y].Value = value;
            }
        }
    }
}
