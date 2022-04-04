using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.MyShapes
{
    internal class SGroupShape : Shape
    {
        public List<SMultiShape> GroupShapes { get; set; }

        public SGroupShape()
        {
            GroupShapes = new List<SMultiShape>();
        }
        public override void AddPoint(Point p)
        {
            
        }

        public void AddShape(SMultiShape s)
        {
            GroupShapes.Add(s);
        }

        public override void DrawShape(Graphics graphics)
        {
            GroupShapes.ForEach(groupShape  =>
            {
                groupShape.DrawShape(graphics);
            });
        }
    }
}
