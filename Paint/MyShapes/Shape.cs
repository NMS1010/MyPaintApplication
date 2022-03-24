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
        /// <summary>
        /// Biến bool cho phép kiểm tra việc vẽ các đường phức tạp như: polygon, curve, path.. đã dừng chưa
        /// </summary>
        public bool IsStopDrawing { get; set; } = false;

        /// <summary>
        /// Biến bool cho phép kiểm tra một hình có được chọn hay chưa
        /// </summary>
        public bool IsChosen { get; set; } = false;
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
        public void SelectedBaseOnRectangle(Graphics graphics, Point a, Point b, Point c, Point d)
        {
            float temp = PenDraw.Width / 1.5F;
            if (temp < 6.0)
            {
                temp = 6.0F;
            }
            Brush brush = new SolidBrush(Color.Black);
            graphics.FillRectangle(brush, a.X, a.Y, temp, temp);
            graphics.FillRectangle(brush, b.X, b.Y, temp, temp);
            graphics.FillRectangle(brush, c.X, c.Y, temp, temp);
            graphics.FillRectangle(brush, d.X, d.Y, temp, temp);
        }
        public void SelectedComplexShape(Graphics graphics, int gap = 1)
        {
            float temp = PenDraw.Width / 1.5F;
            if (temp < 6.0)
            {
                temp = 6.0F;
            }
            Brush b = new SolidBrush(Color.Black);

            for (int i = 0; i < ListPoint.Count; i += gap)
            {
                graphics.FillRectangle(b, ListPoint[i].X - 3, ListPoint[i].Y - 3, temp, temp);
            }
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
    }
}
