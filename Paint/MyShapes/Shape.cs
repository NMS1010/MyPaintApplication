using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paint.Enums;

namespace Paint.MyShapes
{
    abstract class Shape
    {
        public Pen PenDraw { get; set; }
        public Brush BrushDraw { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public List<Point> ListPoint { get; set; }
        public bool IsStopDrawing { get; set; } = false;
        public Shape()
        {

        }
        public Shape(Pen p)
        {
            PenDraw = p;
        }
        public Shape(Brush b)
        {
            BrushDraw = b;
        }
        public Shape(Pen p, Brush b)
        {
            PenDraw = p;
            BrushDraw = b;
        }
        public bool Contains(Point p)
        {
            Shape check = FindShape(p, this);
            if (check == null)
            {
                return false;
            }
            return true;
        }
        public Shape FindShape(Point p, Shape drawObj)
        {
            using (Bitmap bmp = new Bitmap(1171, 510))
            {
                using (var grp = Graphics.FromImage(bmp))
                {
                    grp.Clear(Color.White);
                    drawObj.DrawShape(grp);
                }
                if (bmp.GetPixel(p.X, p.Y).ToArgb() != Color.White.ToArgb())
                {
                    return drawObj;
                }
            }
            
            return null;
        }
        public Rectangle GetSuitableDirectionShape(SHAPE currShape)
        {
            Size sizeRect = new Size(Math.Abs(End.X - Start.X), Math.Abs(End.Y - Start.Y));
            Point p;

            int minLen = Math.Min(sizeRect.Width, sizeRect.Height);
            if (Start.Y > End.Y)
            {
                //Phải dưới - trái trên
                if (Start.X > End.X)
                {
                    p = End;
                    if (currShape == SHAPE.SQUARE || currShape == SHAPE.CIRCLE)
                    {
                        if (sizeRect.Width > sizeRect.Height)
                        {
                            p.X += (sizeRect.Width - minLen);
                        }
                        else
                        {
                            p.Y += (sizeRect.Height - minLen);
                        }
                    }
                }
                //Trái dưới - phải trên
                else
                {
                    p = new Point(Start.X, End.Y);
                    if (currShape == SHAPE.SQUARE || currShape == SHAPE.CIRCLE)
                    {
                        if (sizeRect.Width < sizeRect.Height)
                        {
                            p.Y += (sizeRect.Height - minLen);
                        }
                    }
                }
            }
            else
            {
                //Trái trên - phải dưới
                if (Start.X < End.X)
                {
                    p = Start;
                }
                //Phải trên - trái dưới
                else
                {
                    p = new Point(End.X, Start.Y);
                    if (currShape == SHAPE.SQUARE || currShape == SHAPE.CIRCLE)
                    {
                        if (sizeRect.Width > sizeRect.Height)
                        {
                            p.X += (sizeRect.Width - minLen);
                        }
                    }
                }
            }
            if (currShape == SHAPE.SQUARE || currShape == SHAPE.CIRCLE)
            {
                sizeRect.Width = minLen;
                sizeRect.Height = minLen;
            }
            return new Rectangle(p, sizeRect);
        }
        public abstract void DrawShape(Graphics graphics);
        public abstract void AddPoint(Point p);
        private SRectangle GetRectangle(Point a)
        {
            Pen p = new Pen(Color.Black);
            SRectangle r1 = new SRectangle(p);
            r1.Start = new Point(a.X - 5, a.Y - 5);
            r1.End = new Point(a.X + 5, a.Y + 5);
            return r1;
        }
        public virtual List<Shape> ShapeSelected(Point a, Point b, Point c, Point d)
        {
            List<Shape> shapeSelected = new List<Shape>();
            shapeSelected.Add(GetRectangle(a));
            shapeSelected.Add(GetRectangle(b));
            shapeSelected.Add(GetRectangle(c));
            shapeSelected.Add(GetRectangle(d));
            return shapeSelected;
        }
    }
}
