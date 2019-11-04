namespace Engine
{
    public class Tile: Coordinate
    {
        public int ID { get; set; }
        public enum Value { empty, X, O };
        public Tile(int id, int X, int Y, Value value) : base(X, Y)
        {
            ID = id;
        }
    }
}