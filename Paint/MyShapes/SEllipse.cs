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
        public SEllipse(Pen p) : base(p)
        {

        }
        public override void AddPoint(Point p)
        {
            End = p;
        }

        public override void DrawShape(Graphics graphics)
        {
            Rectangle rect = GetSuitableDirectionShape(SHAPE.ELLIPSE);
            graphics.DrawEllipse(PenDraw, rect);
        }
    }
}
