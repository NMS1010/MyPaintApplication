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

        public void AddShape(SMultiShape s)
        {
            if (s.Shapes.Count == 0) return;
            SMultiShape n = new SMultiShape();
            s.Shapes.ForEach(x => n.AddShape(x));
            n.UpdatePoint();
            GroupShapes.Add(n);
        }

        public override void DrawShape(Graphics graphics)
        {
            GroupShapes.ForEach(groupShape  =>
            {
                //graphics.DrawRectangle(new Pen(Color.Black) { DashStyle = DashStyle.DashDot, Width = 2F },
                //    groupShape.TopLeftPoint.X, groupShape.TopLeftPoint.Y, groupShape.BottomRightPoint.X - groupShape.TopLeftPoint.X,
                //    groupShape.BottomRightPoint.Y - groupShape.TopLeftPoint.Y);
                groupShape.DrawShape(graphics);
            });
        }
    }
}
