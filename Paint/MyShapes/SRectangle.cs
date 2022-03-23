using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Enums;
namespace Paint.MyShapes
{
    class SRectangle : Shape
    {
        public SRectangle(Pen p) : base(p)
        {

        }
        public override void AddPoint(Point p)
        {
            End = p;
        }


        public override void DrawShape(Graphics graphics)
        {
            Rectangle rect = GetSuitableDirectionShape(SHAPE.RECTANGLE);
            graphics.DrawRectangle(PenDraw, rect);
        }
    }
}
