using System.Windows.Forms;

namespace Engine
{
    public class Tile: Coordinate
    {
        public enum TileValue { E, X, O };
        public int ID { get; set; }
        public TileValue Value { get; set; }
        public Panel Panel { get; set; }
        public Tile(int id, int X, int Y, TileValue value, Panel panel) : base(X, Y)
        {
            ID = id;
            Value = value;
            Panel = panel;
        }
        public bool CheckTileState()
        {
            if (Value == TileValue.E)
                return true;
            else
                return false;
        }
        public void refresh()
        {
            Panel.Refresh();
        }
    }
}