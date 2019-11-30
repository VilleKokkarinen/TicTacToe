using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine
{
    /// <summary>
    /// Props for drawing tools
    /// <para> LineWidth, Margin, Size </para>
    /// </summary>
    public class Params
    {
        public int LineWidth { get; set; } = 3;
        public int Margin { get; set; } = 5;
        public int Size { get; set; } = 50;
    }
}
