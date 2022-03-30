using Paint.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SEllipse : Shape
    {
        public SEllipse(Pen p, Brush b, bool isFilled) : base(p, b, isFilled)
        {

        }
        public override void AddPoint(Point p)
        {
            End = p;
        }
        public override void DrawShape(Graphics graphics)
        {
            Rectangle rect = GetSuitableDirectionShape(SHAPE.ELLIPSE);
            if (!IsFilled)
                graphics.DrawEllipse(PenDraw, rect);
            else
            {
                graphics.DrawEllipse(PenDraw, rect);
                graphics.FillEllipse(BrushDraw, rect);
            }
            if (IsChosen)
            {
                float temp = PenDraw.Width / 1.5F;
                if (temp < 6.0)
                {
                    temp = 6.0F;
                }
                Point a, b, c, d;
                a = new Point(Start.X - (int)temp / 2, Start.Y + (End.Y - Start.Y) / 2 - (int)temp / 2);
                b = new Point(Start.X + (End.X - Start.X) / 2 - 3, Start.Y - (int)temp / 2);
                c = new Point(End.X - (int)temp / 2, Start.Y + (End.Y - Start.Y) / 2 - 3);
                d = new Point(End.X - (End.X - Start.X) / 2 - 3, End.Y - 3);
                SelectedBaseOnRectangle(graphics, a, b, c, d);
            }
        }
    }
}
