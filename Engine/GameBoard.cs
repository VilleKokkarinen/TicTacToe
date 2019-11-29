using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class GameBoard
    {
        private Tile[,] Board;

        public GameBoard(int Size = 3)
        {
            Board = new Tile[Size, Size];
            MakeBoardOfSize(Size);
        }

        private void MakeBoardOfSize(int Size = 3)
        {
           int ID = 0;

           for(int X = 0; X < Size; X++)
           {
                for(int Y = 0; Y < Size; Y++)
                {
                    Board[X, Y] = new Tile(ID, 0, 0, Tile.TileValue.NaN, new System.Windows.Forms.Panel());
                    ID++;
                }
           }
        }

        public Tile[,] ReturnBoard()
        {
            return Board;
        }
    }
}
