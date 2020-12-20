using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshot
{
    public partial class ScreenshotForm : Form
    {
        private const int ResizeBoundaryWidth = 3;
        private const int ToolbarSpacing = 8;

        private int preX;
        private int preY;

        private int curX;
        private int curY;

        private bool isDraw;
        private string curAction = "Default";

        private Rectangle curRect;
        private Pen penD;
        private Pen penE;

        private int originX;
        private int originY;
        private Rectangle moveRect;
        private Image background;
        private Rectangle bounds;

        public Image ScreenshotImage { get; private set; }

        public ScreenshotForm()
        {
            InitializeComponent();

            this.tbnSize.ToolTipText = Properties.Resources.Size;
            this.tbnCopy.ToolTipText = Properties.Resources.Done;
            this.tbnExit.ToolTipText = Properties.Resources.Quit;

            this.background = GetAllScreenImage(out this.bounds);
            this.Size = bounds.Size;
            this.BackgroundImage = background;
            this.penD = new Pen(Color.Black, 1);
            this.penE = new Pen(new TextureBrush(background));
        }

        private void ScreenshotForm_Load(object sender, EventArgs e)
        {
            this.Location = bounds.Location;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Gray)), new Rectangle(Point.Empty, bounds.Size));
            e.Graphics.DrawImage(this.background, this.curRect, this.curRect, GraphicsUnit.Pixel);
            if (this.curRect.Height * this.curRect.Width > 0)
                e.Graphics.DrawRectangle(this.penE, this.curRect);
            if (this.curRect.Height * this.curRect.Width > 0)
                e.Graphics.DrawRectangle(this.penD, this.curRect);
        }

        private Bitmap GetAllScreenImage(out Rectangle bounds)
        {
            var screens = Screen.AllScreens;
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;
            foreach (var screen in screens)
            {
                x1 = Math.Min(x1, screen.Bounds.X);
                x2 = Math.Max(x2, screen.Bounds.X + screen.Bounds.Width);
                y1 = Math.Min(y1, screen.Bounds.Y);
                y2 = Math.Max(y2, screen.Bounds.Y + screen.Bounds.Height);
            }
            Bitmap bitmap = new Bitmap(x2 - x1, y2 - y1);
            Graphics g = Graphics.FromImage(bitmap);
            g.CopyFromScreen(x1, y1, 0, 0, new Size(x2 - x1, y2 - y1));
            g.Dispose();
            bounds = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            return bitmap;
        }

        private void ScreenshotForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.HideToolBar();
            this.isDraw = true;
            switch (this.curAction)
            {
                case "Default":
                    if (this.curRect.Width * this.curRect.Height > 0)
                    {
                        this.isDraw = false;
                        return;
                    }
                    this.preX = e.X;
                    this.preY = e.Y;
                    break;
                case "Move":
                    this.originX = e.X;
                    this.originY = e.Y;
                    this.moveRect = this.curRect;
                    break;
                case "ResizeLeft":
                    this.preX = this.curRect.X + this.curRect.Width;
                    this.preY = this.curRect.Y;
                    this.curY = this.curRect.Y + this.curRect.Height;
                    break;
                case "ResizeRight":
                    this.preX = this.curRect.X;
                    this.preY = this.curRect.Y + this.curRect.Height;
                    this.curY = this.curRect.Y;
                    break;
                case "ResizeTop":
                    this.preX = this.curRect.X + this.curRect.Width;
                    this.preY = this.curRect.Y + this.curRect.Height;
                    this.curX = this.curRect.X;
                    break;
                case "ResizeBottom":
                    this.preX = this.curRect.X;
                    this.preY = this.curRect.Y;
                    this.curX = this.curRect.X + this.curRect.Width;
                    break;
                case "ResizeLeftTop":
                    this.preX = this.curRect.X + this.curRect.Width;
                    this.preY = this.curRect.Y + this.curRect.Height;
                    break;
                case "ResizeLeftBottom":
                    this.preX = this.curRect.X + this.curRect.Width;
                    this.preY = this.curRect.Y;
                    break;
                case "ResizeRightTop":
                    this.preX = this.curRect.X;
                    this.preY = this.curRect.Y + this.curRect.Height;
                    break;
                case "ResizeRightBottom":
                    this.preX = this.curRect.X;
                    this.preY = this.curRect.Y;
                    break;
            }
        }

        private void ScreenshotForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.curRect.Height * this.curRect.Width > 0)
                this.ShowToolBar();
            this.isDraw = false;
            this.curAction = "Default";
        }

        private void ScreenshotForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDraw)
            {
                switch (this.curAction)
                {
                    case "Move":
                        int offsetX = e.X - this.originX;
                        int offsetY = e.Y - this.originY;
                        this.preX = this.moveRect.X + offsetX;
                        this.preY = this.moveRect.Y + offsetY;
                        this.curX = this.moveRect.X + this.moveRect.Width + offsetX;
                        this.curY = this.moveRect.Y + this.moveRect.Height + offsetY;
                        this.DrawRect();
                        break;
                    case "ResizeLeft":
                    case "ResizeRight":
                        this.curX = e.X;
                        this.DrawRect();
                        break;
                    case "ResizeTop":
                    case "ResizeBottom":
                        this.curY = e.Y;
                        this.DrawRect();
                        break;
                    case "Default":
                    case "ResizeLeftTop":
                    case "ResizeLeftBottom":
                    case "ResizeRightTop":
                    case "ResizeRightBottom":
                        this.curX = e.X;
                        this.curY = e.Y;
                        this.DrawRect();
                        break;
                }
            }
            else
            {
                if (this.curRect.Height * this.curRect.Width > 0)
                {
                    if (this.curRect.X < e.X && e.X < this.curRect.X + this.curRect.Width &&
                        this.curRect.Y < e.Y && e.Y < this.curRect.Y + this.curRect.Height)
                    {
                        //Move
                        this.curAction = "Move";
                        this.Cursor = Cursors.SizeAll;
                    }
                    else if (Math.Abs(e.X - this.curRect.X) <= ResizeBoundaryWidth && Math.Abs(e.Y - this.curRect.Y) <= ResizeBoundaryWidth)
                    {
                        //Resize Left-Top
                        this.curAction = "ResizeLeftTop";
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else if (Math.Abs(e.X - this.curRect.X) <= ResizeBoundaryWidth && Math.Abs(e.Y - this.curRect.Y - this.curRect.Height) <= ResizeBoundaryWidth)
                    {
                        //Resize Left-Bottom
                        this.curAction = "ResizeLeftBottom";
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (Math.Abs(e.X - this.curRect.X - this.curRect.Width) <= ResizeBoundaryWidth && Math.Abs(e.Y - this.curRect.Y) <= ResizeBoundaryWidth)
                    {
                        //Resize Right-Top
                        this.curAction = "ResizeRightTop";
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (Math.Abs(e.X - this.curRect.X - this.curRect.Width) <= ResizeBoundaryWidth && Math.Abs(e.Y - this.curRect.Y - this.curRect.Height) <= ResizeBoundaryWidth)
                    {
                        //Resize Right-Bottom
                        this.curAction = "ResizeRightBottom";
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else if (this.curRect.X < e.X && e.X < this.curRect.X + this.curRect.Width &&
                        Math.Abs(e.Y - this.curRect.Y) <= ResizeBoundaryWidth)
                    {
                        //Resize Top
                        this.curAction = "ResizeTop";
                        this.Cursor = Cursors.SizeNS;
                    }
                    else if (this.curRect.X < e.X && e.X < this.curRect.X + this.curRect.Width &&
                        Math.Abs(e.Y - this.curRect.Y - this.curRect.Height) <= ResizeBoundaryWidth)
                    {
                        //Resize Bottom
                        this.curAction = "ResizeBottom";
                        this.Cursor = Cursors.SizeNS;
                    }
                    else if (this.curRect.Y < e.Y && e.Y < this.curRect.Y + this.curRect.Height &&
                        Math.Abs(e.X - this.curRect.X) <= ResizeBoundaryWidth)
                    {
                        //Resize Left
                        this.curAction = "ResizeLeft";
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (this.curRect.Y < e.Y && e.Y < this.curRect.Y + this.curRect.Height &&
                        Math.Abs(e.X - this.curRect.X - this.curRect.Width) <= ResizeBoundaryWidth)
                    {
                        //Resize Right
                        this.curAction = "ResizeRight";
                        this.Cursor = Cursors.SizeWE;
                    }
                    else
                    {
                        this.curAction = "Default";
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void DrawRect()
        {
            this.curRect.X = this.preX < this.curX ? this.preX : this.curX;
            this.curRect.Y = this.preY < this.curY ? this.preY : this.curY;
            this.curRect.Width = Math.Abs(this.preX - this.curX);
            this.curRect.Height = Math.Abs(this.preY - this.curY);
            this.Invalidate();
        }

        private void ShowToolBar()
        {
            Point position = new Point();
            position.X = this.curRect.X + this.curRect.Width - this.imageToolbar.Width;
            position.Y = this.curRect.Y + this.curRect.Height + ToolbarSpacing;
            if (position.X < ToolbarSpacing)
                position.X = ToolbarSpacing;
            else if (position.X > this.Width - this.imageToolbar.Width - ToolbarSpacing)
                position.X = this.Width - this.imageToolbar.Width - ToolbarSpacing;
            if (position.Y > this.Height - this.imageToolbar.Height - ToolbarSpacing)
                position.Y = this.curRect.Y - this.imageToolbar.Height - ToolbarSpacing;
            this.imageToolbar.Location = position;
            this.tbnSize.Text = String.Format("{0}×{1}", this.curRect.Width, this.curRect.Height);
            this.imageToolbar.Visible = true;
        }

        private void HideToolBar()
        {
            this.imageToolbar.Visible = false;
        }

        private void ScreenshotForm_DoubleClick(object sender, EventArgs e)
        {
            if (!this.curRect.Size.IsEmpty)
            {
                if (this.curRect.X + this.bounds.X < Control.MousePosition.X && Control.MousePosition.X < this.curRect.X + this.bounds.X + this.curRect.Width &&
                    this.curRect.Y + this.bounds.Y < Control.MousePosition.Y && Control.MousePosition.Y < this.curRect.Y + this.bounds.Y + this.curRect.Height)
                {
                    this.tbnDone_Click(null, null);
                }
            }
        }

        private void ScreenshotForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        public Image GetCutImage()
        {
            Bitmap image = new Bitmap(this.curRect.Width, this.curRect.Height);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(this.background, new Rectangle(Point.Empty, image.Size), this.curRect, GraphicsUnit.Pixel);
            g.Dispose();
            return image;
        }

        private void tbnDone_Click(object sender, EventArgs e)
        {
            var image = this.GetCutImage();
            ScreenshotImage = image;
            this.DialogResult = DialogResult.OK;
        }

        private void tbnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ScreenshotForm_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Activate();
        }
    }
}
