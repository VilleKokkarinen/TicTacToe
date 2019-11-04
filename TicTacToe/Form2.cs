using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public class Params
    {
        public int LineWidth { get; set; } = 2;
        public int Margin { get; set; } = 2;
        public int Size { get; set; } = 50;
    }

    public partial class Form2 : Form
    {
        private readonly int _LineWidth = new Params().LineWidth;
        private readonly int _Margin = new Params().Margin;
        private readonly int _Size = new Params().Size;

        public Form2()
        {
            InitializeComponent();
        }
        public void DrawCircle(Panel panel)
        {
            int x, y, width, height;
            // marginin verran vasemmasta ylänurkasta
            x = _Margin;
            y = _Margin;
            // Ympyrän koosta vähennetään 2 marginin verran. (Koosta pois 1, ja yhden verran alanurkasta)
            int margin = _Margin*2;
            width = _Size-margin;
            height = _Size-margin;

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
        private void panel_Painter(Panel panel)
        {
            Pen myPen = new Pen(Color.Black);
            panel.CreateGraphics().DrawEllipse(myPen, new Rectangle(panel.Location, panel.Size));
        }
        private void Button1_Click(object sender, EventArgs e)
        { 
            DrawCross(panel1);
            DrawCircle(panel2);
            // DrawCircle(panel1.Location, panel1.Size);
        }
    }
}
