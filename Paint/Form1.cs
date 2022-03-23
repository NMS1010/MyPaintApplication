using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Paint.Enums;
using Paint.MyShapes;

namespace Paint
{
    public partial class Form1 : Form
    {
        private BufferedGraphics grp;
        private BufferedGraphicsContext grpContext;

        private SHAPE currShape = SHAPE.NONE;
        private ACTIONS currActions = ACTIONS.NONE;
        private TOOL currTool = TOOL.PEN;

        private List<Shape> drawShapeObj;
        private SMultiShape multiShape;
        private Point startMouseMovePoint;
        private List<List<Point>> pointsMove;

        private List<Button> buttonsFunc;
        private List<Button> buttonsTool;
        private List<DashStyle> listDashStyles;

        private Color myColor = Color.Red;
        private DashStyle myDashStyle = DashStyle.Solid;
        private int myWidth = 1;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitGraphics();
            Init();
        }
        /// <summary>
        /// Khởi tạo các đối tượng Graphics để chuẩn bị vẽ, dùng BufferedGraphics, BufferedGraphicsContext để tránh bị hiện tượng flicker khi re-render nhiều lần
        /// </summary>
        private void InitGraphics()
        {
            grpContext = BufferedGraphicsManager.Current;
            grpContext.MaximumBuffer = new Size(Width, Height);
            grp = grpContext.Allocate(mainPnl.CreateGraphics(), mainPnl.DisplayRectangle);
            grp.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        private void SetImage()
        {
            color_picker_Ptrb.Image = Image.FromFile(@"../../Image/color_palette.png");
            penBtn.Image = Image.FromFile(@"../../Image/pencil.png");
            fillBtn.Image = Image.FromFile(@"../../Image/bucket.png");
            eraserBtn.Image = Image.FromFile(@"../../Image/eraser.png");
            colorPtrb.BackColor = myColor;
        }
        private void Init()
        {
            
            listDashStyles = new List<DashStyle>();
            foreach(DashStyle d in Enum.GetValues(typeof(DashStyle)))
            {
                listDashStyles.Add(d);
            }
            foreach (Button btn in shapeGrp.Controls)
            {
                btn.Click += ChooseShape_Click;
            }
            foreach (Button btn in toolGrp.Controls)
            {
                btn.Click += ChooseTool_Click;
            }

            drawShapeObj = new List<Shape>();
            multiShape = new SMultiShape();

            //Danh sách Button phải đúng theo thứ tự chức năng trong Enums.SHAPE
            buttonsFunc = new List<Button>() { lineBtn, rectBtn, squareBtn, ellipseBtn, circleBtn, curveBtn, polygonBtn, pathBtn };
            buttonsTool = new List<Button>() { penBtn, fillBtn, eraserBtn};
            buttonsTool[(int)currTool].BackColor = Color.Red;
            pointsMove = new List<List<Point>>();
            SetImage();

            listDashStyles.ForEach(dashStyle => styleCbx.Items.Add(dashStyle));
            styleCbx.SelectedIndex = 0;

            thickTrbar.Value = myWidth;
        }

        private void ChooseTool_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buttonsTool.Count; i++)
            {
                if (buttonsTool[i] == sender)
                {
                    buttonsTool[i].BackColor = Color.Red;
                    currTool = (TOOL)i;
                }
                else
                {
                    buttonsTool[i].BackColor = Color.White;
                }
            }
        }
        private void ChooseShape_Click(object sender, EventArgs e)
        {
            currActions = ACTIONS.READYDRAW;
            bool isChecked = false;
            for(int i = 0; i < buttonsFunc.Count; i++)
            {
                if(buttonsFunc[i] == sender && buttonsFunc[i].Tag == null)
                {
                    buttonsFunc[i].BackColor = Color.Aqua;
                    currShape = (SHAPE)i;
                    buttonsFunc[i].Tag = "Checked";
                    isChecked = true;
                }
                else
                {
                    buttonsFunc[i].BackColor = Color.White;
                    buttonsFunc[i].Tag = null;
                }
            }
            if(!isChecked)
                currShape = SHAPE.NONE;
        }
        private void AddShapeToList()
        {
            Pen p = new Pen(myColor) { DashStyle = myDashStyle, Width = myWidth };
            switch (currShape)
            {
                case SHAPE.LINE: 
                    drawShapeObj.Add(new SLine(p)); 
                    break;
                case SHAPE.RECTANGLE: 
                    drawShapeObj.Add(new SRectangle(p)); 
                    break;
                case SHAPE.SQUARE: 
                    drawShapeObj.Add(new SSquare(p)); 
                    break;
                case SHAPE.ELLIPSE: 
                    drawShapeObj.Add(new SEllipse(p)); 
                    break;
                case SHAPE.CIRCLE: 
                    drawShapeObj.Add(new SCircle(p)); 
                    break;
                case SHAPE.CURVE: 
                    drawShapeObj.Add(new SCurve(p)); 
                    break;
                case SHAPE.POLYGON: 
                    drawShapeObj.Add(new SPolygon(p)); 
                    break;
                case SHAPE.PATH: 
                    drawShapeObj.Add(new SPath(p)); 
                    break;
            }
        }
        private void ReRender()
        {
            grp.Graphics.Clear(Color.White);
            drawShapeObj.ForEach(shape => shape.DrawShape(grp.Graphics));
            grp.Render();
        }
        private void mainPnl_MouseMove(object sender, MouseEventArgs e)
        {
            if (currActions == ACTIONS.DRAWING)
            {
                drawShapeObj[drawShapeObj.Count - 1].End = e.Location;
                ReRender();
            }
            else if (currActions == ACTIONS.READYDRAW || currActions == ACTIONS.NONE || currActions == ACTIONS.GROUPING)
            {
                drawShapeObj.ForEach(shape =>
                {
                    if (shape.Contains(e.Location))
                    {
                        Cursor.Current = Cursors.SizeAll;
                    }
                });
            }
            else if (currActions == ACTIONS.MOVING)
            {
                int dx = e.X - startMouseMovePoint.X;
                int dy = e.Y - startMouseMovePoint.Y;
                for (int i = 0; i < multiShape.Shapes.Count; i++)
                {
                    if (multiShape.Shapes[i] is SPath || multiShape.Shapes[i] is SPolygon || multiShape.Shapes[i] is SCurve)
                    {
                        for (int j = 0; j < multiShape.Shapes[i].ListPoint.Count; j++)
                        {
                            multiShape.Shapes[i].ListPoint[j] = new Point(pointsMove[i][j].X + dx, pointsMove[i][j].Y + dy);
                        }
                    }
                    else
                    {
                        multiShape.Shapes[i].Start = new Point(pointsMove[i][0].X + dx,
                            pointsMove[i][0].Y + dy);
                        multiShape.Shapes[i].End = new Point(pointsMove[i][1].X + dx,
                            pointsMove[i][1].Y + dy);
                    }
                }
                ReRender();
            }
        }
        private void GroupShape(Shape shape)
        {
            multiShape.AddShape(shape);
        }

        private void MainPnl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && currActions != ACTIONS.DRAWING)
            {
                currActions = ACTIONS.GROUPING;
            }
        }
        private void MainPnl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey && currActions != ACTIONS.DRAWING)
            {
                currActions = ACTIONS.NONE;
            }
        }
        private void mainPnl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) {
                return; 
            }

            startMouseMovePoint = e.Location;
            if (currShape == SHAPE.NONE)
            {
                if (currActions == ACTIONS.GROUPING)
                {
                    drawShapeObj.ForEach(shape =>
                    {
                        if (shape.Contains(e.Location))
                        {
                            GroupShape(shape);
                        }
                    });
                }
                else
                {
                    bool flag = false;

                    pointsMove.Clear();
                    multiShape.Shapes.ForEach(shape =>
                    {
                        if (shape.Contains(e.Location))
                        {
                            flag = true;
                        }
                        List<Point> temp = new List<Point>();
                        if(shape is SPath || shape is SPolygon || shape is SCurve)
                        {
                            for(int i = 0; i < shape.ListPoint.Count; i++)
                            {
                                temp.Add(new Point(shape.ListPoint[i].X, shape.ListPoint[i].Y));
                            }
                        }
                        else
                        {
                            Point startPoint = new Point(shape.Start.X, shape.Start.Y);
                            Point endPoint = new Point(shape.End.X, shape.End.Y);
                            temp = new List<Point>() { startPoint, endPoint };
                        }
                        pointsMove.Add(temp);
                    });
                    if (!flag) return;
                    currActions = ACTIONS.MOVING;
                }
                return;
            }
            if(currActions != ACTIONS.DRAWING)
                AddShapeToList();
            currActions = ACTIONS.DRAWING;
            drawShapeObj[drawShapeObj.Count - 1].Start = e.Location;
            if (currShape == SHAPE.PATH || currShape == SHAPE.CURVE || currShape == SHAPE.POLYGON)
            {
                drawShapeObj[drawShapeObj.Count - 1].AddPoint(e.Location);
            }
        }

        private void mainPnl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (currActions == ACTIONS.DRAWING)
            {
                drawShapeObj[drawShapeObj.Count - 1].End = e.Location;
                if (currShape != SHAPE.PATH && currShape != SHAPE.CURVE && currShape != SHAPE.POLYGON)
                {
                    currActions = ACTIONS.READYDRAW;
                }
                ReRender();
            }
            else
            {
                currActions = ACTIONS.NONE;
            }
        }
        private void mainPnl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (currShape == SHAPE.PATH || currShape == SHAPE.CURVE || currShape == SHAPE.POLYGON)
            {
                currActions = ACTIONS.READYDRAW;
                drawShapeObj[drawShapeObj.Count - 1].End = e.Location;
                drawShapeObj[drawShapeObj.Count - 1].IsStopDrawing = true;
                drawShapeObj[drawShapeObj.Count - 1].AddPoint(e.Location);
                ReRender();
            }
        }
        /// <summary>
        /// Điều chỉnh vị trí con trỏ chuột sao cho phù hợp với độ Scale của Image trong PictureBox (như SizeMode: StretchImage, Zoom...)
        /// </summary>
        /// <param name="point"> Vị trí con trỏ chuột hiện tại</param>
        /// <returns></returns>
        private Point GetPointWithScale(Point point)
        {
            float scaleX = (float)color_picker_Ptrb.Image.Width / color_picker_Ptrb.Width;
            float scaleY = (float)color_picker_Ptrb.Image.Height / color_picker_Ptrb.Height;
            int x = (int)(scaleX * point.X);
            int y = (int)(scaleY * point.Y);
            return new Point(x, y);
        }
        private void color_picker_Ptrb_MouseClick(object sender, MouseEventArgs e)
        {
            Point pointTemp = GetPointWithScale(e.Location);
            Color newColor = (color_picker_Ptrb.Image as Bitmap).GetPixel(pointTemp.X, pointTemp.Y);
            myColor = newColor;
            colorPtrb.BackColor = newColor;
        }

        private void styleCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            myDashStyle = (DashStyle)styleCbx.Items[styleCbx.SelectedIndex];
        }

        private void thickTrbar_Scroll(object sender, EventArgs e)
        {
            myWidth = (sender as TrackBar).Value; 
        }

    }
}
