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
                    if (Shapes[i].Start.X < xMin)
                    {
                        xMin = Shapes[i].Start.X;
                    }
                    if (Shapes[i].Start.Y < yMin)
                    {
                        yMin = Shapes[i].Start.Y;
                    }
                    if (Shapes[i].End.X > xMax)
                    {
                        xMax = Shapes[i].End.X;
                    }
                    if (Shapes[i].End.Y > yMax)
                    {
                        yMax = Shapes[i].End.Y;
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
