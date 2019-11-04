using System.ComponentModel;
using System.Collections.Generic;

namespace Engine
{
    public class Board : INotifyPropertyChanged
    {
        private Tile _details;
        private int _quantity;
        private static readonly Tile[,] _Board = new Tile[,] {
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

        public Tile Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }

        public Board(Tile details)
        {
            Details = details;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}