
namespace Paint
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.propGrp = new System.Windows.Forms.GroupBox();
            this.styleCbx = new System.Windows.Forms.ComboBox();
            this.thickTrbar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorPickerGrp = new System.Windows.Forms.GroupBox();
            this.color_picker_Ptrb = new System.Windows.Forms.PictureBox();
            this.toolGrp = new System.Windows.Forms.GroupBox();
            this.fillBtn = new System.Windows.Forms.Button();
            this.eraserBtn = new System.Windows.Forms.Button();
            this.penBtn = new System.Windows.Forms.Button();
            this.colorPtrb = new System.Windows.Forms.PictureBox();
            this.shapeGrp = new System.Windows.Forms.GroupBox();
            this.pathBtn = new System.Windows.Forms.Button();
            this.polygonBtn = new System.Windows.Forms.Button();
            this.curveBtn = new System.Windows.Forms.Button();
            this.circleBtn = new System.Windows.Forms.Button();
            this.squareBtn = new System.Windows.Forms.Button();
            this.ellipseBtn = new System.Windows.Forms.Button();
            this.rectBtn = new System.Windows.Forms.Button();
            this.lineBtn = new System.Windows.Forms.Button();
            this.mainPnl = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.propGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thickTrbar)).BeginInit();
            this.colorPickerGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.color_picker_Ptrb)).BeginInit();
            this.toolGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPtrb)).BeginInit();
            this.shapeGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.BlueViolet;
            this.panel1.Controls.Add(this.propGrp);
            this.panel1.Controls.Add(this.colorPickerGrp);
            this.panel1.Controls.Add(this.toolGrp);
            this.panel1.Controls.Add(this.colorPtrb);
            this.panel1.Controls.Add(this.shapeGrp);
            this.panel1.Location = new System.Drawing.Point(-3, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1171, 157);
            this.panel1.TabIndex = 0;
            // 
            // propGrp
            // 
            this.propGrp.Controls.Add(this.styleCbx);
            this.propGrp.Controls.Add(this.thickTrbar);
            this.propGrp.Controls.Add(this.label2);
            this.propGrp.Controls.Add(this.label1);
            this.propGrp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propGrp.Location = new System.Drawing.Point(883, 6);
            this.propGrp.Name = "propGrp";
            this.propGrp.Size = new System.Drawing.Size(204, 114);
            this.propGrp.TabIndex = 5;
            this.propGrp.TabStop = false;
            this.propGrp.Text = "Properties";
            // 
            // styleCbx
            // 
            this.styleCbx.FormattingEnabled = true;
            this.styleCbx.Location = new System.Drawing.Point(61, 20);
            this.styleCbx.Name = "styleCbx";
            this.styleCbx.Size = new System.Drawing.Size(121, 21);
            this.styleCbx.TabIndex = 3;
            this.styleCbx.SelectedIndexChanged += new System.EventHandler(this.styleCbx_SelectedIndexChanged);
            // 
            // thickTrbar
            // 
            this.thickTrbar.Location = new System.Drawing.Point(61, 59);
            this.thickTrbar.Maximum = 20;
            this.thickTrbar.Name = "thickTrbar";
            this.thickTrbar.Size = new System.Drawing.Size(104, 45);
            this.thickTrbar.TabIndex = 2;
            this.thickTrbar.Scroll += new System.EventHandler(this.thickTrbar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Style";
            // 
            // colorPickerGrp
            // 
            this.colorPickerGrp.Controls.Add(this.color_picker_Ptrb);
            this.colorPickerGrp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorPickerGrp.Location = new System.Drawing.Point(614, 6);
            this.colorPickerGrp.Name = "colorPickerGrp";
            this.colorPickerGrp.Size = new System.Drawing.Size(233, 148);
            this.colorPickerGrp.TabIndex = 4;
            this.colorPickerGrp.TabStop = false;
            this.colorPickerGrp.Text = "Color Picker";
            // 
            // color_picker_Ptrb
            // 
            this.color_picker_Ptrb.Location = new System.Drawing.Point(6, 19);
            this.color_picker_Ptrb.Name = "color_picker_Ptrb";
            this.color_picker_Ptrb.Size = new System.Drawing.Size(221, 123);
            this.color_picker_Ptrb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.color_picker_Ptrb.TabIndex = 1;
            this.color_picker_Ptrb.TabStop = false;
            this.color_picker_Ptrb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.color_picker_Ptrb_MouseClick);
            // 
            // toolGrp
            // 
            this.toolGrp.Controls.Add(this.fillBtn);
            this.toolGrp.Controls.Add(this.eraserBtn);
            this.toolGrp.Controls.Add(this.penBtn);
            this.toolGrp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolGrp.Location = new System.Drawing.Point(15, 36);
            this.toolGrp.Name = "toolGrp";
            this.toolGrp.Size = new System.Drawing.Size(203, 84);
            this.toolGrp.TabIndex = 3;
            this.toolGrp.TabStop = false;
            this.toolGrp.Text = "Tool";
            // 
            // fillBtn
            // 
            this.fillBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.fillBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fillBtn.Location = new System.Drawing.Point(72, 14);
            this.fillBtn.Name = "fillBtn";
            this.fillBtn.Size = new System.Drawing.Size(60, 60);
            this.fillBtn.TabIndex = 2;
            this.fillBtn.Text = "Fill";
            this.fillBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.fillBtn.UseVisualStyleBackColor = false;
            // 
            // eraserBtn
            // 
            this.eraserBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eraserBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.eraserBtn.Location = new System.Drawing.Point(138, 15);
            this.eraserBtn.Name = "eraserBtn";
            this.eraserBtn.Size = new System.Drawing.Size(60, 60);
            this.eraserBtn.TabIndex = 1;
            this.eraserBtn.Text = "Eraser";
            this.eraserBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.eraserBtn.UseVisualStyleBackColor = false;
            // 
            // penBtn
            // 
            this.penBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.penBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.penBtn.Location = new System.Drawing.Point(6, 14);
            this.penBtn.Name = "penBtn";
            this.penBtn.Size = new System.Drawing.Size(60, 60);
            this.penBtn.TabIndex = 0;
            this.penBtn.Text = "Pen";
            this.penBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.penBtn.UseVisualStyleBackColor = false;
            // 
            // colorPtrb
            // 
            this.colorPtrb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPtrb.Location = new System.Drawing.Point(591, 25);
            this.colorPtrb.Name = "colorPtrb";
            this.colorPtrb.Size = new System.Drawing.Size(17, 123);
            this.colorPtrb.TabIndex = 2;
            this.colorPtrb.TabStop = false;
            // 
            // shapeGrp
            // 
            this.shapeGrp.Controls.Add(this.pathBtn);
            this.shapeGrp.Controls.Add(this.polygonBtn);
            this.shapeGrp.Controls.Add(this.curveBtn);
            this.shapeGrp.Controls.Add(this.circleBtn);
            this.shapeGrp.Controls.Add(this.squareBtn);
            this.shapeGrp.Controls.Add(this.ellipseBtn);
            this.shapeGrp.Controls.Add(this.rectBtn);
            this.shapeGrp.Controls.Add(this.lineBtn);
            this.shapeGrp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.shapeGrp.Location = new System.Drawing.Point(248, 6);
            this.shapeGrp.Name = "shapeGrp";
            this.shapeGrp.Size = new System.Drawing.Size(308, 148);
            this.shapeGrp.TabIndex = 0;
            this.shapeGrp.TabStop = false;
            this.shapeGrp.Text = "Shapes";
            // 
            // pathBtn
            // 
            this.pathBtn.BackColor = System.Drawing.Color.White;
            this.pathBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pathBtn.Location = new System.Drawing.Point(79, 81);
            this.pathBtn.Name = "pathBtn";
            this.pathBtn.Size = new System.Drawing.Size(55, 55);
            this.pathBtn.TabIndex = 7;
            this.pathBtn.Text = "Path";
            this.pathBtn.UseVisualStyleBackColor = false;
            // 
            // polygonBtn
            // 
            this.polygonBtn.BackColor = System.Drawing.Color.White;
            this.polygonBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.polygonBtn.Location = new System.Drawing.Point(7, 80);
            this.polygonBtn.Name = "polygonBtn";
            this.polygonBtn.Size = new System.Drawing.Size(55, 55);
            this.polygonBtn.TabIndex = 6;
            this.polygonBtn.Text = "Polygon";
            this.polygonBtn.UseVisualStyleBackColor = false;
            // 
            // curveBtn
            // 
            this.curveBtn.BackColor = System.Drawing.Color.White;
            this.curveBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.curveBtn.Location = new System.Drawing.Point(154, 81);
            this.curveBtn.Name = "curveBtn";
            this.curveBtn.Size = new System.Drawing.Size(55, 55);
            this.curveBtn.TabIndex = 5;
            this.curveBtn.Text = "Curve";
            this.curveBtn.UseVisualStyleBackColor = false;
            // 
            // circleBtn
            // 
            this.circleBtn.BackColor = System.Drawing.Color.White;
            this.circleBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.circleBtn.Location = new System.Drawing.Point(154, 20);
            this.circleBtn.Name = "circleBtn";
            this.circleBtn.Size = new System.Drawing.Size(55, 55);
            this.circleBtn.TabIndex = 4;
            this.circleBtn.Text = "Circle";
            this.circleBtn.UseVisualStyleBackColor = false;
            // 
            // squareBtn
            // 
            this.squareBtn.BackColor = System.Drawing.Color.White;
            this.squareBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.squareBtn.Location = new System.Drawing.Point(230, 19);
            this.squareBtn.Name = "squareBtn";
            this.squareBtn.Size = new System.Drawing.Size(55, 55);
            this.squareBtn.TabIndex = 3;
            this.squareBtn.Text = "Square";
            this.squareBtn.UseVisualStyleBackColor = false;
            // 
            // ellipseBtn
            // 
            this.ellipseBtn.BackColor = System.Drawing.Color.White;
            this.ellipseBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ellipseBtn.Location = new System.Drawing.Point(79, 20);
            this.ellipseBtn.Name = "ellipseBtn";
            this.ellipseBtn.Size = new System.Drawing.Size(55, 55);
            this.ellipseBtn.TabIndex = 2;
            this.ellipseBtn.Text = "Ellipse";
            this.ellipseBtn.UseVisualStyleBackColor = false;
            // 
            // rectBtn
            // 
            this.rectBtn.BackColor = System.Drawing.Color.White;
            this.rectBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rectBtn.Location = new System.Drawing.Point(230, 80);
            this.rectBtn.Name = "rectBtn";
            this.rectBtn.Size = new System.Drawing.Size(55, 55);
            this.rectBtn.TabIndex = 1;
            this.rectBtn.Text = "Rectangle";
            this.rectBtn.UseVisualStyleBackColor = false;
            // 
            // lineBtn
            // 
            this.lineBtn.BackColor = System.Drawing.Color.White;
            this.lineBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lineBtn.Location = new System.Drawing.Point(7, 20);
            this.lineBtn.Name = "lineBtn";
            this.lineBtn.Size = new System.Drawing.Size(55, 55);
            this.lineBtn.TabIndex = 0;
            this.lineBtn.Text = "Line";
            this.lineBtn.UseVisualStyleBackColor = false;
            // 
            // mainPnl
            // 
            this.mainPnl.BackColor = System.Drawing.Color.White;
            this.mainPnl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mainPnl.Location = new System.Drawing.Point(-3, 189);
            this.mainPnl.Name = "mainPnl";
            this.mainPnl.Size = new System.Drawing.Size(1171, 510);
            this.mainPnl.TabIndex = 1;
            this.mainPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPnl_Paint);
            this.mainPnl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mainPnl_MouseClick);
            this.mainPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPnl_MouseDown);
            this.mainPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPnl_MouseMove);
            this.mainPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPnl_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 699);
            this.Controls.Add(this.mainPnl);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainPnl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainPnl_KeyUp);
            this.panel1.ResumeLayout(false);
            this.propGrp.ResumeLayout(false);
            this.propGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thickTrbar)).EndInit();
            this.colorPickerGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.color_picker_Ptrb)).EndInit();
            this.toolGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorPtrb)).EndInit();
            this.shapeGrp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox shapeGrp;
        private System.Windows.Forms.Button curveBtn;
        private System.Windows.Forms.Button circleBtn;
        private System.Windows.Forms.Button squareBtn;
        private System.Windows.Forms.Button ellipseBtn;
        private System.Windows.Forms.Button rectBtn;
        private System.Windows.Forms.Button lineBtn;
        private System.Windows.Forms.Button pathBtn;
        private System.Windows.Forms.Button polygonBtn;
        private System.Windows.Forms.GroupBox toolGrp;
        private System.Windows.Forms.Button fillBtn;
        private System.Windows.Forms.Button eraserBtn;
        private System.Windows.Forms.Button penBtn;
        private System.Windows.Forms.PictureBox colorPtrb;
        private System.Windows.Forms.PictureBox color_picker_Ptrb;
        private System.Windows.Forms.GroupBox colorPickerGrp;
        private System.Windows.Forms.GroupBox propGrp;
        private System.Windows.Forms.ComboBox styleCbx;
        private System.Windows.Forms.TrackBar thickTrbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel mainPnl;
    }
}

