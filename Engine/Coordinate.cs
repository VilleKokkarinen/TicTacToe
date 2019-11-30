using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// Pretty self explanatory ?
    /// </summary>
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
