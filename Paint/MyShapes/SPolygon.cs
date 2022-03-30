using Paint.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SPolygon : Shape
    {
        public SPolygon(Pen p, Brush b, bool isFilled) : base(p, b, isFilled)
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
            {
                ListPoint.RemoveAt(ListPoint.Count - 1);
                return;
            }
            if (!IsFilled)
                graphics.DrawPolygon(PenDraw, ListPoint.ToArray());
            else
            {
                graphics.DrawPolygon(PenDraw, ListPoint.ToArray());
                graphics.FillPolygon(BrushDraw, ListPoint.ToArray());
            }
            
            if (IsStopDrawing == false)
                ListPoint.RemoveAt(ListPoint.Count - 1);
            if (IsChosen)
            {
                SelectedComplexShape(graphics);
            }
        }
    }
}
