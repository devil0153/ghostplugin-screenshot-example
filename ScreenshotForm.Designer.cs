
namespace Screenshot
{
    partial class ScreenshotForm
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
            this.imageToolbar = new System.Windows.Forms.ToolStrip();
            this.tbnSize = new System.Windows.Forms.ToolStripLabel();
            this.tbnSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.tbnExit = new System.Windows.Forms.ToolStripButton();
            this.tbnCopy = new System.Windows.Forms.ToolStripButton();
            this.imageToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageToolbar
            // 
            this.imageToolbar.BackColor = System.Drawing.Color.White;
            this.imageToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.imageToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.imageToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbnSize,
            this.tbnSep0,
            this.tbnExit,
            this.tbnCopy});
            this.imageToolbar.Location = new System.Drawing.Point(-18, 39);
            this.imageToolbar.Name = "imageToolbar";
            this.imageToolbar.Size = new System.Drawing.Size(115, 25);
            this.imageToolbar.TabIndex = 2;
            this.imageToolbar.Text = "toolStrip1";
            this.imageToolbar.Visible = false;
            // 
            // tbnSize
            // 
            this.tbnSize.AutoSize = false;
            this.tbnSize.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnSize.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tbnSize.Name = "tbnSize";
            this.tbnSize.Size = new System.Drawing.Size(60, 22);
            // 
            // tbnSep0
            // 
            this.tbnSep0.BackColor = System.Drawing.Color.White;
            this.tbnSep0.ForeColor = System.Drawing.Color.Silver;
            this.tbnSep0.Name = "tbnSep0";
            this.tbnSep0.Size = new System.Drawing.Size(6, 25);
            // 
            // tbnExit
            // 
            this.tbnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnExit.Image = global::Screenshot.Properties.Resources.Cancel;
            this.tbnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnExit.Name = "tbnExit";
            this.tbnExit.Size = new System.Drawing.Size(23, 22);
            this.tbnExit.Click += new System.EventHandler(this.tbnExit_Click);
            // 
            // tbnCopy
            // 
            this.tbnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnCopy.Image = global::Screenshot.Properties.Resources.OK;
            this.tbnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnCopy.Name = "tbnCopy";
            this.tbnCopy.Size = new System.Drawing.Size(23, 22);
            this.tbnCopy.Click += new System.EventHandler(this.tbnDone_Click);
            // 
            // ScreenshotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.Controls.Add(this.imageToolbar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreenshotForm";
            this.ShowInTaskbar = false;
            this.Text = "ScreenshotForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ScreenshotForm_Load);
            this.Shown += new System.EventHandler(this.ScreenshotForm_Shown);
            this.DoubleClick += new System.EventHandler(this.ScreenshotForm_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenshotForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScreenshotForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenshotForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScreenshotForm_MouseUp);
            this.imageToolbar.ResumeLayout(false);
            this.imageToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip imageToolbar;
        private System.Windows.Forms.ToolStripLabel tbnSize;
        private System.Windows.Forms.ToolStripSeparator tbnSep0;
        private System.Windows.Forms.ToolStripButton tbnExit;
        private System.Windows.Forms.ToolStripButton tbnCopy;
    }
}