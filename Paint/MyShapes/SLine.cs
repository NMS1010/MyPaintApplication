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
        }
    }
}
