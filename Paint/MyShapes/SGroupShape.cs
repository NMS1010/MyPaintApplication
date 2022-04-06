using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

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
        private bool ContainMultiShape(SMultiShape s)
        {
            int count = 0;
            for(int i = 0; i < GroupShapes.Count; i++)
            {
                foreach(Shape j in s.Shapes)
                {
                    if (GroupShapes[i].Shapes.Contains(j))
                    {
                        count++;
                    }
                }
            }

            if (count == s.Shapes.Count)
            {
                return true;
            }
            return false;
        }
        public SMultiShape GroupContainShape(Shape s)
        {
            for (int i = 0; i < GroupShapes.Count; i++)
            {
                if (GroupShapes[i].Shapes.Contains(s))
                {
                    return GroupShapes[i];
                }
            }
            return null;
        }
        public void AddShape(SMultiShape s)
        {
            if (s.Shapes.Count == 0 || ContainMultiShape(s))
            {
                return;
            }
            SMultiShape n = new SMultiShape();
            s.Shapes.ForEach(x => n.AddShape(x));
            n.UpdatePoint();
            GroupShapes.Add(n);
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
