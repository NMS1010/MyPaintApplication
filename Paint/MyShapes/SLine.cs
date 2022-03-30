using Paint.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SLine : Shape
    {
        
        public SLine(Pen p): base(p)
        {

        }

        public override void AddPoint(Point p)
        {
            End = p;
        }
        public override void DrawShape(Graphics graphics)
        {
            graphics.DrawLine(PenDraw, Start, End);
            if (IsChosen)
            {
                float temp = PenDraw.Width / 1.5F;
                if (temp < 6.0)
                {
                    temp = 6.0F;
                }
                Brush b = new SolidBrush(Color.Black);
                graphics.FillRectangle(b, Start.X - 3, Start.Y - 3, temp, temp );
                graphics.FillRectangle(b, Start.X + (End.X - Start.X) / 2 - 3, Start.Y + (End.Y - Start.Y) /2 - 3, temp, temp );
                graphics.FillRectangle(b, End.X - 5, End.Y - 5, temp , temp );
            }
        }
    }
}
