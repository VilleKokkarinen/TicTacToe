using System.Drawing;
using System.Windows.Forms;

namespace Engine
{
    public class Drawing
    {
        private readonly int _LineWidth = new Params().LineWidth;
        private readonly int _Margin = new Params().Margin;
        private readonly int _Size = new Params().Size;

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
        public void DrawCross(Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            Pen _pen = new Pen(Color.Black, _LineWidth);
            // viiva \
            g.DrawLine(_pen, new Point(_Margin, _Margin), new Point(_Size - _Margin, _Size - _Margin));
            // viiva /
            g.DrawLine(_pen, new Point(_Margin, _Size - _Margin), new Point(_Size - _Margin, _Margin));

        }
        public void DrawWinnerLineVertical(Panel pnl1, Panel pnl2, Panel pnl3)
        {
            Graphics g = pnl1.CreateGraphics();
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);

            g.DrawLine(_pen, new Point(_Size/2, 0), new Point(_Size/2, _Size));
            g = pnl2.CreateGraphics();
            g.DrawLine(_pen, new Point(_Size / 2, 0), new Point(_Size / 2, _Size));
            g = pnl3.CreateGraphics();
            g.DrawLine(_pen, new Point(_Size / 2, 0), new Point(_Size / 2, _Size));
        }
        public void DrawWinnerLineHorizontal(Panel pnl1, Panel pnl2, Panel pnl3)
        {
            Graphics g = pnl1.CreateGraphics();
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);

            g.DrawLine(_pen, new Point(0, _Size / 2), new Point(_Size, _Size / 2));
            g = pnl2.CreateGraphics();
            g.DrawLine(_pen, new Point(0, _Size / 2), new Point(_Size, _Size / 2));
            g = pnl3.CreateGraphics();
            g.DrawLine(_pen, new Point(0, _Size / 2), new Point(_Size, _Size / 2));
        }
        public void DrawWinnerLineDiagonal(Panel pnl1, Panel pnl2, Panel pnl3)
        {
            Graphics g = pnl1.CreateGraphics();
            Pen _pen = new Pen(Color.Green, _LineWidth * 2);

            g.DrawLine(_pen, new Point(0, 0), new Point(_Size, _Size));
            g = pnl2.CreateGraphics();
            g.DrawLine(_pen, new Point(0, 0), new Point(_Size, _Size));
            g = pnl3.CreateGraphics();
            g.DrawLine(_pen, new Point(0, 0), new Point(_Size, _Size));
        }
        public void DrawWinnerLineAntiDiagonal(Panel pnl1, Panel pnl2, Panel pnl3)
        {
            Graphics g = pnl1.CreateGraphics();
            Pen _pen = new Pen(Color.Green, _LineWidth*2);

            g.DrawLine(_pen, new Point(_Size, 0), new Point(0, _Size));
            g = pnl2.CreateGraphics();
            g.DrawLine(_pen, new Point(_Size, 0), new Point(0, _Size));
            g = pnl3.CreateGraphics();
            g.DrawLine(_pen, new Point(_Size, 0), new Point(0, _Size));
        }
    }
}