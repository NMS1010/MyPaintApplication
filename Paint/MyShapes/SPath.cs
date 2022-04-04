using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Paint.Enums;

namespace Paint.MyShapes
{
    class SPath : Shape
    {
        private GraphicsPath graphicsPath;
        public SPath(Pen p) : base(p)
        {
            ListPoint = new List<Point>();
        }


        public override void AddPoint(Point p)
        {
            ListPoint.Add(p);
        }

        public override void DrawShape(Graphics graphics)
        {

            if (IsStopDrawing == false)
                ListPoint.Add(End);
            if (ListPoint.Count < 2)
                return;
            graphicsPath = new GraphicsPath();
            graphicsPath.AddLines(ListPoint.ToArray());
            graphics.DrawPath(PenDraw, graphicsPath);
            if (IsChosen)
            {
                if (IsZoom)
                {
                    float temp = PenDraw.Width / 1.5F;
                    if (temp < 6.0)
                    {
                        temp = 6.0F;
                    }
                    Point a, b, c, d;
                    a = new Point(TopLeftPoint.X - (int)temp / 2, TopLeftPoint.Y - (int)temp / 2);
                    b = new Point(BottomRightPoint.X - 3, TopLeftPoint.Y - (int)temp / 2);
                    c = new Point(TopLeftPoint.X - (int)temp / 2, BottomRightPoint.Y - 3);
                    d = new Point(BottomRightPoint.X - 3, BottomRightPoint.Y - 3);
                    SelectedBaseOnRectangle(graphics, a, b, c, d);
                }
                else
                {
                    int gap = ListPoint.Count / 4;
                    SelectedComplexShape(graphics, gap);
                }
            }
            
        }
    }
}
