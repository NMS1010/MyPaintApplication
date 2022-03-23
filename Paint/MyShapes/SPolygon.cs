﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SPolygon : Shape
    {
        public SPolygon(Pen p) : base(p)
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
            graphics.DrawPolygon(PenDraw, ListPoint.ToArray());
            if (IsStopDrawing == false)
                ListPoint.RemoveAt(ListPoint.Count - 1);
        }
    }
}
