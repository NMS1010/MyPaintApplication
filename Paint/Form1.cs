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
        private COLOR_TYPE currColorType = COLOR_TYPE.PENCOLOR;

        private List<Shape> drawShapeObj;
        private SMultiShape multiShape;
        private Point startMouseMovePoint;
        private List<List<Point>> pointsMove;

        private List<Button> buttonsFunc;
        private List<Button> buttonsTool;

        private Color myColor = Color.Red;
        private DashStyle myDashStyle = DashStyle.Solid;
        private int myWidth = 1;

        private string currBrushStyle;
        private HatchStyle currHatchStyle;
        public LinearGradientMode currLinearGradientMode;
        private Brush currBrush;
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

            foreach (DashStyle d in Enum.GetValues(typeof(DashStyle)))
            {
                styleCbx.Items.Add(d);
            }

            foreach (HatchStyle name in Enum.GetValues(typeof(HatchStyle)))
            {
                hatchStyleCbx.Items.Add(name);
            }
            foreach (LinearGradientMode mode in Enum.GetValues(typeof(LinearGradientMode)))
            {
                linearGradientStyleCbx.Items.Add(mode);
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
            buttonsTool = new List<Button>() { penBtn, fillBtn, eraserBtn };
            buttonsTool[(int)currTool].BackColor = Color.Red;

            backColorPtrb.BackColor = myColor;
            penColorPtrb.BackColor = myColor;
            foreColorPtrb.BackColor = myColor;

            startColorGradientPtrb.BackColor = Color.Yellow;
            endColorGradientPtrb.BackColor = Color.Green;  

            pointsMove = new List<List<Point>>();
            SetImage();


            styleCbx.SelectedIndex = 0;
            brushStyleCbx.SelectedIndex = 0;
            thickTrbar.Value = myWidth;
            hatchStyleCbx.SelectedIndex = 0;
            linearGradientStyleCbx.SelectedIndex = 0;
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
            for (int i = 0; i < buttonsFunc.Count; i++)
            {
                if (buttonsFunc[i] == sender && buttonsFunc[i].Tag == null)
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
            if (!isChecked)
                currShape = SHAPE.NONE;
        }
        private void AddShapeToList()
        {
            Pen penDraw = new Pen(penColorPtrb.BackColor) { DashStyle = myDashStyle, Width = myWidth };
            //Brush b = new SolidBrush(backColorPtrb.BackColor);
            switch (currShape)
            {
                case SHAPE.LINE:
                    drawShapeObj.Add(new SLine(penDraw));
                    break;
                case SHAPE.RECTANGLE:
                    drawShapeObj.Add(new SRectangle(penDraw, currBrush, currTool == TOOL.FILL));
                    break;
                case SHAPE.SQUARE:
                    drawShapeObj.Add(new SSquare(penDraw, currBrush, currTool == TOOL.FILL));
                    break;
                case SHAPE.ELLIPSE:
                    drawShapeObj.Add(new SEllipse(penDraw, currBrush, currTool == TOOL.FILL));
                    break;
                case SHAPE.CIRCLE:
                    drawShapeObj.Add(new SCircle(penDraw, currBrush, currTool == TOOL.FILL));
                    break;
                case SHAPE.CURVE:
                    drawShapeObj.Add(new SCurve(penDraw));
                    break;
                case SHAPE.POLYGON:
                    drawShapeObj.Add(new SPolygon(penDraw, currBrush, currTool == TOOL.FILL));
                    break;
                case SHAPE.PATH:
                    drawShapeObj.Add(new SPath(penDraw));
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
            ReRender();
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
            if (e.Button != MouseButtons.Left)
            {
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
                            flag = true;
                        List<Point> temp = new List<Point>();
                        if (shape is SPath || shape is SPolygon || shape is SCurve)
                        {
                            for (int i = 0; i < shape.ListPoint.Count; i++)
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
            if (currActions != ACTIONS.DRAWING)
                AddShapeToList();
            currActions = ACTIONS.DRAWING;
            drawShapeObj[drawShapeObj.Count - 1].Start = e.Location;
            if (currShape == SHAPE.PATH || currShape == SHAPE.CURVE || currShape == SHAPE.POLYGON)
            {
                drawShapeObj[drawShapeObj.Count - 1].AddPoint(e.Location);
            }
        }
        private void mainPnl_Click(object sender, EventArgs e)
        {
            if (currShape != SHAPE.NONE || currActions == ACTIONS.GROUPING || currActions == ACTIONS.MOVING)
            {
                return;
            }
            MouseEventArgs e2 = e as MouseEventArgs;
            if (e2.Button != MouseButtons.Left) return;
            multiShape.Shapes.ForEach(selectedShape => selectedShape.IsChosen = false);
            multiShape.Shapes.Clear();
            drawShapeObj.ForEach(shape =>
            {
                if (shape.Contains(e2.Location))
                {
                    GroupShape(shape);
                }
            });
            ReRender();

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
        /// Điều chỉnh vị trí con trỏ chuột sao cho phù hợp với độ Scale của Image trong PictureBox (PictureBox co SizeMode: StretchImage, Zoom...)
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

            if (currColorType == COLOR_TYPE.BACKCOLOR)
            {
                backColorPtrb.BackColor = myColor;
            }
            else if (currColorType == COLOR_TYPE.PENCOLOR)
            {
                penColorPtrb.BackColor = myColor;
            }
            else if (currColorType == COLOR_TYPE.FORECOLOR)
            {
                foreColorPtrb.BackColor = myColor;
            }
            else if (currColorType == COLOR_TYPE.START_COLOR)
            {
                startColorGradientPtrb.BackColor = myColor;
            }
            else if (currColorType != COLOR_TYPE.END_COLOR)
            {
                endColorGradientPtrb.BackColor = myColor;
            }
            currBrush = GetBrush();
            multiShape.Shapes.ForEach(shape =>
            {
                shape.PenDraw.Color = penColorPtrb.BackColor;
                if (currColorType != COLOR_TYPE.PENCOLOR)
                    shape.BrushDraw = GetBrush();
            });
            ReRender();
        }

        private void styleCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            myDashStyle = (DashStyle)styleCbx.Items[styleCbx.SelectedIndex];
            multiShape.Shapes.ForEach(shape =>
            {
                shape.PenDraw.DashStyle = myDashStyle;
            });
            ReRender();
        }

        private void thickTrbar_Scroll(object sender, EventArgs e)
        {
            myWidth = (sender as TrackBar).Value;
            multiShape.Shapes.ForEach(shape =>
            {
                shape.PenDraw.Width = myWidth;
            });
            ReRender();
        }

        private void mainPnl_Paint(object sender, PaintEventArgs e)
        {
            ReRender();
        }

        private void RefreshPtrbs()
        {
            penColorPtrb.Refresh();
            backColorPtrb.Refresh();
            foreColorPtrb.Refresh();
            startColorGradientPtrb.Refresh();
            endColorGradientPtrb.Refresh();
        }

        private void backColorPtrb_Click(object sender, EventArgs e)
        {
            currColorType = COLOR_TYPE.BACKCOLOR;
            RefreshPtrbs();
        }

        private void penColorPtrb_Click(object sender, EventArgs e)
        {
            currColorType = COLOR_TYPE.PENCOLOR;
            RefreshPtrbs();
        }

        private void foreColorPtrb_Click(object sender, EventArgs e)
        {
            currColorType = COLOR_TYPE.FORECOLOR;
            RefreshPtrbs();
        }
        private void startColorGradientPtrb_Click(object sender, EventArgs e)
        {
            currColorType = COLOR_TYPE.START_COLOR;
            RefreshPtrbs();
        }

        private void endColorGradientPtrb_Click(object sender, EventArgs e)
        {
            currColorType = COLOR_TYPE.END_COLOR;
            RefreshPtrbs();
        }
        public void ChangeBorderPtrb(PaintEventArgs e, COLOR_TYPE type)
        {
            if (currColorType == type)
                ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, Border3DStyle.Flat);
            else
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, myColor, ButtonBorderStyle.None);
        }
        private void penColorPtrb_Paint(object sender, PaintEventArgs e)
        {
            ChangeBorderPtrb(e, COLOR_TYPE.PENCOLOR);
        }
        private void backColorPtrb_Paint(object sender, PaintEventArgs e)
        {
            ChangeBorderPtrb(e, COLOR_TYPE.BACKCOLOR);
        }
        private void foreColorPtrb_Paint(object sender, PaintEventArgs e)
        {
            ChangeBorderPtrb(e, COLOR_TYPE.FORECOLOR);
        }
        private void startColorGradientPtrb_Paint(object sender, PaintEventArgs e)
        {
            ChangeBorderPtrb(e, COLOR_TYPE.START_COLOR);
        }

        private void endColorGradientPtrb_Paint(object sender, PaintEventArgs e)
        {
            ChangeBorderPtrb(e, COLOR_TYPE.END_COLOR);
        }
        private Brush GetBrushStyle(string style, Color? backColor = null, Color? foreColor = null, HatchStyle? hatchStyle = null,
            Color? startColor = null, Color? endColor = null, LinearGradientMode? linearGradientMode = null, Rectangle? rect = null)
        {
            switch (style)
            {
                case "SolidBrush":
                    return new SolidBrush(backColor.Value);
                case "HatchBrush":
                    return new HatchBrush(hatchStyle.Value, foreColor.Value, backColor.Value);
                case "LinearGradientBrush":
                    return new LinearGradientBrush(rect.Value, startColor.Value, endColor.Value, linearGradientMode.Value);
            }

            return null;
        }
        private Brush GetBrush()
        {
            Brush br = null;
            switch (currBrushStyle)
            {
                case "SolidBrush":
                    br = GetBrushStyle(currBrushStyle, backColor: backColorPtrb.BackColor);
                    break;
                case "HatchBrush":
                    br = GetBrushStyle(currBrushStyle, backColor: backColorPtrb.BackColor, foreColor: foreColorPtrb.BackColor, hatchStyle: currHatchStyle);
                    break;
                case "LinearGradientBrush":
                    br = GetBrushStyle(currBrushStyle, rect: new Rectangle(50,50,50,50), startColor: startColorGradientPtrb.BackColor, endColor: endColorGradientPtrb.BackColor, linearGradientMode: currLinearGradientMode);
                    break;
            }
            return br;
        }

        private void GetCurrentBrush()
        {
            currBrush = GetBrush();
            multiShape.Shapes.ForEach(shape =>
            {
                shape.BrushDraw = GetBrush();
            });
            ReRender();
        }
        private void brushStyleCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            currBrushStyle = brushStyleCbx.Items[brushStyleCbx.SelectedIndex].ToString();
            GetCurrentBrush();
            if (currBrushStyle == "SolidBrush")
            {
                hatchBrushGrp.Visible = false;
                linearGradientBrushGrp.Visible = false;
            }else if (currBrushStyle == "HatchBrush")
            {
                hatchBrushGrp.Visible = true;
                linearGradientBrushGrp.Visible = false;
            }else if (currBrushStyle == "LinearGradientBrush")
            {
                hatchBrushGrp.Visible = false;
                linearGradientBrushGrp.Visible = true;
            }
        }

        private void hatchStyleCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            currHatchStyle = (HatchStyle)hatchStyleCbx.Items[hatchStyleCbx.SelectedIndex];
            GetCurrentBrush();
        }

        private void linearGradientStyleCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            currLinearGradientMode = (LinearGradientMode)linearGradientStyleCbx.Items[linearGradientStyleCbx.SelectedIndex];
            GetCurrentBrush();
        }

    }
}
