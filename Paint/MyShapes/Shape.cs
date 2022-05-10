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

        /// <summary>
        /// Xác định giới hạn trái trên cho các hình khi đang di chuyển để vẽ
        /// </summary>
        public Point TopLeftPoint { get; set; }
        /// <summary>
        /// Xác định giới hạn phải dưới cho các hình khi đang di chuyển để vẽ
        /// </summary>
        public Point BottomRightPoint { get; set; }
        /// <summary>
        /// Danh sách các điểm khi vẽ các hình phức tạp
        /// </summary>
        public List<Point> ListPoint { get; set; }
        /// <summary>
        /// Biến bool cho phép đánh dấu việc vẽ các đường phức tạp như: polygon, curve, path.. đã hoàn thành chưa
        /// </summary>
        public bool IsStopDrawing { get; set; } = false;

        /// <summary>
        /// Biến bool đánh dấu một hình có đang được chọn hay không
        /// </summary>
        public bool IsChosen { get; set; } = false;

        /// <summary>
        /// Biến bool đánh dấu một hình có đang được tô hay không
        /// </summary>
        public bool IsFilled { get; set; } = false;

        /// <summary>
        /// Biến bool đánh dấu một hình có đang được zoom hay không
        /// </summary>
        public bool IsZoom { get; set; } = false;

        public Shape()
        {

        }
        public Shape(Pen p)
        {
            PenDraw = p;
            
        }

        public Shape(Pen p, Brush b, bool isFilled)
        {
            PenDraw = p;
            BrushDraw = b;
            IsFilled = isFilled;
        }

        public Point GetTopLeftPoint()
        {
            Point pMin = new Point(ListPoint[0].X, ListPoint[0].Y);
            Point minY = ListPoint[0];
            for (int i = 0; i < ListPoint.Count; i++)
            {
                if(pMin.Y > ListPoint[i].Y)
                {
                    pMin.Y = ListPoint[i].Y;
                }
                if(minY.X > ListPoint[i].X)
                {
                    minY = ListPoint[i];
                }
            }
            pMin.X = minY.X - 20;
            pMin.Y = pMin.Y - 20;
            return pMin;
        }
        public Point GetBottomRightPoint()
        {
            Point pMax = new Point(ListPoint[0].X, ListPoint[0].Y);
            Point maxX = ListPoint[0];
            for (int i = 0; i < ListPoint.Count; i++)
            {
                if (pMax.Y < ListPoint[i].Y)
                {
                    pMax.Y = ListPoint[i].Y;
                }
                if (maxX.X < ListPoint[i].X)
                {
                    maxX = ListPoint[i];
                }
            }
            pMax.X = maxX.X + 20;
            pMax.Y = pMax.Y + 20;
            return pMax;
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
        private Shape FindShape(Point p, Shape drawObj)
        {
            Form1 temp = new Form1();
            using (Bitmap bmp = new Bitmap(temp.mainPnl.Width, temp.mainPnl.Height))
            {
                using (var grp = Graphics.FromImage(bmp))
                {
                    grp.Clear(Color.White);
                    drawObj.DrawShape(grp);
                }
                try
                {
                    if (bmp.GetPixel(p.X, p.Y).ToArgb() != Color.White.ToArgb())
                    {
                        return drawObj;
                    }
                }
                catch { }
            }

            return null;
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
        public bool ContainBound(Point p)
        {
            return FindBound(p, this) == null;
        }
        private Shape FindBound(Point p, Shape drawObj)
        {
            Form1 temp = new Form1();
            using (Bitmap bmp = new Bitmap(temp.mainPnl.Width, temp.mainPnl.Height))
            {
                using (var grp = Graphics.FromImage(bmp))
                {
                    grp.Clear(Color.White);
                    drawObj.DrawShape(grp);
                }
                try
                {
                    if (bmp.GetPixel(p.X, p.Y).ToArgb() != Color.Black.ToArgb())
                    {
                        return drawObj;
                    }
                }
                catch { }
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
