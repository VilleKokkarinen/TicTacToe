using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// A Tile on the gameboard
    /// </summary>
    public class Tile: Coordinate
    {
        // enums of possible values of a tile
        public enum TileValue { NaN, X, O };

        // Id of tile
        public int ID { get; set; }

        // value of the tile
        public TileValue Value { get; set; }

        // panel of the tile
        public Panel Panel { get; set; }

        /// <summary>
        /// Create a Tile with given parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="value"></param>
        /// <param name="panel"></param>
        public Tile(int id, int X, int Y, TileValue value, Panel panel) : base(X, Y)
        {
            ID = id;
            Value = value;
            Panel = panel;
        }

        /// <summary>
        /// Check tile state, returns true if empty
        /// </summary>
        /// <returns></returns>
        public bool CheckTileState()
        {
            if (Value == TileValue.NaN)
                return true;
            else
                return false;
        }
    }
}