using Paint.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SCircle : SEllipse
    {
        public SCircle(Pen p, Brush b, bool isFilled) : base(p, b, isFilled)
        {

        }
        public override void AddPoint(Point p)
        {
            End = p;
        }


        public override void DrawShape(Graphics graphics)
        {
            Rectangle rect = GetSuitableDirectionShape(SHAPE.CIRCLE);
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
                int edge = rect.Width;
                a = new Point(rect.X - (int)temp / 2, rect.Y + edge / 2 - (int)temp / 2);
                b = new Point(rect.X + edge / 2 - 3, rect.Y - (int)temp / 2);
                c = new Point(rect.X + edge - (int)temp / 2, rect.Y + edge / 2 - 3);
                d = new Point(rect.X + edge / 2 - 3, rect.Y + edge - 3);
                SelectedBaseOnRectangle(graphics, a, b, c, d);
            }
        }
    }
}
