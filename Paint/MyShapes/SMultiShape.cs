using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    class SMultiShape : Shape
    {
        private List<Shape> shapes;

        internal List<Shape> Shapes { get => shapes; set => shapes = value; }

        public SMultiShape()
        {
            Shapes = new List<Shape>();
        }
        public override void AddPoint(Point p)
        {
            throw new NotImplementedException();
        }
        public void AddShape(Shape shape)
        {
            if(!Shapes.Contains(shape))
                Shapes.Add(shape);
            else
                Shapes.Remove(shape);
        }
        public override void DrawShape(Graphics graphics)
        {
            Shapes.ForEach(shape => shape.DrawShape(graphics));
        }

    }
}
