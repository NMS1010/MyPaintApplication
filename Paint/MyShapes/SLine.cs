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
            Rectangle rect = GetSuitableDirectionShape(SHAPE.LINE);
            if (IsChosen)
            {
                float temp = PenDraw.Width / 1.5F;
                if (temp < 6.0)
                {
                    temp = 6.0F;
                }
                Brush br = new SolidBrush(Color.Black);
                Point a, b, c;
                if (IsZoom)
                {
                    a = new Point(Start.X - 3, Start.Y - 3);
                    c = new Point(End.X - 5, End.Y - 5);
                }
                else
                {
                    a = new Point(Start.X - 3, Start.Y - 3);
                    b = new Point(Start.X + (End.X - Start.X) / 2 - 3, Start.Y + (End.Y - Start.Y) / 2 - 3);
                    c = new Point(End.X - 5, End.Y - 5);
                    graphics.FillRectangle(br, b.X, b.Y, temp, temp);
                }
                graphics.FillRectangle(br, a.X, a.Y, temp, temp );
                graphics.FillRectangle(br, c.X, c.Y, temp , temp );
            }
            TopLeftPoint = new Point(rect.Left, rect.Top);
            BottomRightPoint = new Point(rect.Right, rect.Bottom);
        }
    }
}
