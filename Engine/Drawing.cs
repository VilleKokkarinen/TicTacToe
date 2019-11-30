using System.Drawing;
using System.Windows.Forms;

namespace Engine
{

    /// <summary>
    /// Drawing functionalities
    /// </summary>
    public class Drawing
    {
        private readonly int _LineWidth = new Params().LineWidth;
        private readonly int _Margin = new Params().Margin;
        private readonly int _Size = new Params().Size;

        /// <summary>
        /// Draws a circle on the panel
        /// </summary>
        /// <param name="panel"></param>
        public void DrawCircle(Panel panel)
        {
            int x, y, width, height;
            // marginin verran vasemmasta ylänurkasta
            x = _Margin;
            y = _Margin;
            // Ympyrän koosta vähennetään 2 marginin verran. (1 alanurkasta, + 1 koosta)
            width = _Size - _Margin * 2;
            height = _Size - _Margin * 2;

            Graphics g = panel.CreateGraphics();
            Pen _pen = new Pen(Color.Black, _LineWidth);

            g.DrawEllipse(_pen, x, y, width, height);
        }

        /// <summary>
        /// Draws a Cross on the panel
        /// </summary>
        /// <param name="panel"></param>
        public void DrawCross(Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            Pen _pen = new Pen(Color.Black, _LineWidth);
            // viiva \
            g.DrawLine(_pen, new Point(_Margin, _Margin), new Point(_Size - _Margin, _Size - _Margin));
            // viiva /
            g.DrawLine(_pen, new Point(_Margin, _Size - _Margin), new Point(_Size - _Margin, _Margin));
        }

        /// <summary>
        /// Draws a vertical "|" line on the given panels
        /// </summary>
        /// <param name="panels"></param>
        public void DrawVerticalLine(Panel[] panels)
        {
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);
            foreach (Panel p in panels)
            {
                Graphics g = p.CreateGraphics();
                g.DrawLine(_pen, new Point(0, _Size / 2), new Point(_Size, _Size / 2));
            }
        }

        /// <summary>
        /// Draws a horizontal "-" Line on the given panels
        /// </summary>
        /// <param name="panels"></param>
        public void DrawHorizontalLine(Panel[] panels)
        {
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);
            foreach (Panel p in panels)
            {
                Graphics g = p.CreateGraphics();
                g.DrawLine(_pen, new Point(_Size / 2, 0), new Point(_Size / 2, _Size));
            }
        }

        /// <summary>
        /// Draws a diagonal "\" line on the given panels
        /// </summary>
        /// <param name="panels"></param>
        public void DrawDiagonalLine(Panel[] panels)
        {
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);
            foreach (Panel p in panels)
            {
                Graphics g = p.CreateGraphics();
                g.DrawLine(_pen, new Point(0, 0), new Point(_Size, _Size));
            }
        }

        /// <summary>
        /// Draws a AntiDiagonal "/" Line on the given panels
        /// </summary>
        /// <param name="panels"></param>
        public void DrawAntiDiagonalLine(Panel[] panels)
        {
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);
            foreach (Panel p in panels)
            {
                Graphics g = p.CreateGraphics();

                g.DrawLine(_pen, new Point(_Size, 0), new Point(0, _Size));
            }
        }
    }
}