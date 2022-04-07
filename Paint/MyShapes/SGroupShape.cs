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
            int count;
            for(int i = 0; i < GroupShapes.Count; i++)
            {
                count = 0;
                foreach (Shape j in s.Shapes)
                {
                    if (GroupShapes[i].Shapes.Contains(j))
                    {
                        count++;
                    }
                }
                if (count == s.Shapes.Count)
                {
                    return true;
                }
            }

            return false;
        }
        public List<SMultiShape> GroupsContainShape(Shape s)
        {
            List< SMultiShape > groupShapes = new List<SMultiShape>();
            for (int i = 0; i < GroupShapes.Count; i++)
            {
                if (GroupShapes[i].Shapes.Contains(s))
                {
                    groupShapes.Add(GroupShapes[i]);
                }
            }
            return groupShapes;
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
