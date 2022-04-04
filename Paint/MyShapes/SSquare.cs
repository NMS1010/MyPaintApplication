using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Enums;

namespace Paint.MyShapes
{
    class SSquare : SRectangle
    {
        public SSquare(Pen p, Brush b, bool isFilled) : base(p, b, isFilled)
        {

        }
        public override void AddPoint(Point p)
        {
            End = p;
        }

        public override void DrawShape(Graphics graphics)
        {
            Rectangle rect = GetSuitableDirectionShape(SHAPE.SQUARE);
            if (!IsFilled)
                graphics.DrawRectangle(PenDraw, rect);
            else
            {
                graphics.DrawRectangle(PenDraw, rect);
                graphics.FillRectangle(BrushDraw, rect);
            }
            if (IsChosen)
            {
                float temp = PenDraw.Width / 1.5F;
                if (temp < 6.0)
                {
                    temp = 6.0F;
                }
                int edge = rect.Width;
                Point a, b, c, d;
                a = new Point(rect.X - (int)temp / 2, rect.Y - (int)temp / 2);
                b = new Point(rect.X + edge - 3, rect.Y - (int)temp / 2);
                c = new Point(rect.X - (int)temp / 2, rect.Y + edge - 3);
                d = new Point(rect.X + edge - 3, rect.Y + edge - 3);
                SelectedBaseOnRectangle(graphics, a, b, c, d);
            }

            this.End = new Point(rect.Right, rect.Bottom);
        }

    }
}
