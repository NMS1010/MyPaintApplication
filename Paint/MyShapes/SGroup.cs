using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    internal class SGroup : Shape
    {
        public List<SGroupShape> Shapes { get; set; }

        public SGroup()
        {
            Shapes = new List<SGroupShape>();
        }
        public override void AddPoint(Point p)
        {
            
        }
        public void AddShape(SGroupShape s)
        {
            Shapes.Add(s);
        }
        public override void DrawShape(Graphics graphics)
        {
            Shapes.ForEach(shape =>
            {
                shape.DrawShape(graphics);
            });
        }
    }
}
