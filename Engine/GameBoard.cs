﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class GameBoard
    {
        private static readonly Tile[,] Board = new Tile[3, 3]
        {
            { new Tile(0, 0, 0, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(3, 0, 1, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(6, 0, 2, Tile.TileValue.NaN, new System.Windows.Forms.Panel()) },
            { new Tile(1, 1, 0, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(4, 1, 1, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(7, 1, 2, Tile.TileValue.NaN, new System.Windows.Forms.Panel()) },
            { new Tile(2, 2, 0, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(5, 2, 1, Tile.TileValue.NaN, new System.Windows.Forms.Panel()), new Tile(8, 2, 2, Tile.TileValue.NaN, new System.Windows.Forms.Panel()) }
        };

        static GameBoard()
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
