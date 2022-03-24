﻿using System;
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
            if (IsChosen)
            {
                float temp = PenDraw.Width / 1.5F;
                if (temp < 6.0)
                {
                    temp = 6.0F;
                }
                Point a, b, c, d;
                a = new Point(Start.X - (int)temp / 2, Start.Y - (int)temp / 2);
                b = new Point(End.X - 3, Start.Y - (int)temp / 2);
                c = new Point(Start.X - (int)temp / 2, End.Y - 3);
                d = new Point(End.X - 3, End.Y - 3);
                SelectedBaseOnRectangle(graphics, a, b, c, d);
            }
        }
    }
}