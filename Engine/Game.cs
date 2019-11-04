using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Game
    {
        private static readonly Tile[,] Board = new Tile[,] {
            { new Tile(0, 0, 0, Tile.Value.empty) },
            { new Tile(0, 0, 0, Tile.Value.empty) },
            { new Tile(1, 0, 1, Tile.Value.empty) },
            { new Tile(2, 0, 2, Tile.Value.empty) },
            { new Tile(3, 1, 0, Tile.Value.empty) },
            { new Tile(4, 1, 1, Tile.Value.empty) },
            { new Tile(5, 1, 2, Tile.Value.empty) },
            { new Tile(6, 2, 0, Tile.Value.empty) },
            { new Tile(7, 2, 1, Tile.Value.empty) },
            { new Tile(8, 2, 2, Tile.Value.empty) },
        };

        static Game()
        {
            PopulateBoard();
        }

        private static void PopulateBoard()
        {
          
        }

        public static Tile[,] ReturnBoard()
        {
            return Board;
        }
    }
}
