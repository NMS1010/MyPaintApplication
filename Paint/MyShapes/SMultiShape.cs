using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SMultiShape : Shape
    {
        private List<Shape> shapes;
        
        public List<Shape> Shapes { get => shapes; set => shapes = value; }

        public SMultiShape()
        {
            Shapes = new List<Shape>();
        }
        public override void AddPoint(Point p)
        {
            
        }

        public static bool operator ==(SMultiShape a, SMultiShape b) 
        {
            if(a.Shapes.Count != b.Shapes.Count) return false;
            for(int i = 0; i < a.Shapes.Count; i++)
            {
                if(a.Shapes[i] != b.Shapes[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator !=(SMultiShape a, SMultiShape b)
        {
            if (a.Shapes.Count == b.Shapes.Count) return false;
            for (int i = 0; i < a.Shapes.Count; i++)
            {
                if (a.Shapes[i] == b.Shapes[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void UpdatePoint()
        {
            int xMin = 999999;
            int yMin = 999999;
            int xMax = -999999;
            int yMax = -999999;
            for (int i = 0; i < Shapes.Count; i++)
            {
                if (Shapes[i] is SPath || Shapes[i] is SPolygon || Shapes[i] is SCurve)
                {
                    Shapes[i].ListPoint.ForEach(point => {
                        if (point.X < xMin)
                        {
                            xMin = point.X;
                        }
                        if (point.Y < yMin)
                        {
                            yMin = point.Y;
                        }
                        if (point.X > xMax)
                        {
                            xMax = point.X;
                        }
                        if (point.Y > yMax)
                        {
                            yMax = point.Y;
                        }
                    });
                }
                else
                {
                    if (Shapes[i].TopLeftPoint.X < xMin)
                    {
                        xMin = Shapes[i].TopLeftPoint.X;
                    }
                    if (Shapes[i].TopLeftPoint.Y < yMin)
                    {
                        yMin = Shapes[i].TopLeftPoint.Y;
                    }
                    if (Shapes[i].BottomRightPoint.X > xMax)
                    {
                        xMax = Shapes[i].BottomRightPoint.X;
                    }
                    if (Shapes[i].BottomRightPoint.Y > yMax)
                    {
                        yMax = Shapes[i].BottomRightPoint.Y;
                    }
                }
            }
            TopLeftPoint = new Point(xMin, yMin);
            BottomRightPoint = new Point(xMax, yMax);
        }
        public void AddShape(Shape shape)
        {
            if (!Shapes.Contains(shape))
            {
                Shapes.Add(shape);
                shape.IsChosen = true;
            }
            else
            {
                Shapes.Remove(shape);
                shape.IsChosen = false;
            }

        }
        public override void DrawShape(Graphics graphics)
        {

            Shapes.ForEach(shape => shape.DrawShape(graphics));
        }

    }
}
