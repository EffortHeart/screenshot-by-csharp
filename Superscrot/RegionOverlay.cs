using System;
using System.Drawing;
using System.Windows.Forms;

namespace Superscrot
{
    /// <summary>
    /// Allows the user to select a region on the active screen to screenshot.
    /// </summary>
    public partial class RegionOverlay : Form
    {
        private Screen _screen;
        private MouseEventArgs _start = null;

        /// <summary>
        /// Initializes a new region overlay with the colors from the program configuration.
        /// </summary>
        public RegionOverlay()
        {
            InitializeComponent();
            this.BackColor = Program.Config.OverlayBackgroundColor;
            if (Program.Config.OverlayForegroundColor.A == 0)
            {
                if (this.BackColor != Color.HotPink)
                {
                    this.ForeColor = Color.HotPink;
                }
                else
                {
                    this.ForeColor = Color.CornflowerBlue;
                }
                this.TransparencyKey = this.ForeColor;
            }
            else
            {
                this.ForeColor = Program.Config.OverlayForegroundColor;
                this.TransparencyKey = Color.Empty;
            }
            this.Opacity = Program.Config.OverlayOpacity;
        }

        /// <summary>
        /// Gets the region selected by the user.
        /// </summary>
        public Rectangle SelectedRegion { get; private set; }

        /// <summary>
        /// Gets a rectangle based on the start position and the current mouse position.
        /// </summary>
        private Rectangle GetRectangle(MouseEventArgs e)
        {
            int left = 0;
            int top = 0;
            int right = 0;
            int bottom = 0;

            if (_start != null)
            {
                if (_start.X < Cursor.Position.X)
                {
                    left = _start.X;
                    right = Cursor.Position.X;
                }
                else
                {
                    left = Cursor.Position.X;
                    right = _start.X;
                }
                if (_start.Y < Cursor.Position.Y)
                {
                    top = _start.Y;
                    bottom = Cursor.Position.Y;
                }
                else
                {
                    top = Cursor.Position.Y;
                    bottom = _start.Y;
                }

                // Include the end-point so that selecting the whole screen
                // actually selects the whole screen, and not everything except
                // one pixel on each axis.
                return Rectangle.FromLTRB(left, top, right + 1, bottom + 1);
            }
            return Rectangle.Empty;
        }

        private void RegionOverlay_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void RegionOverlay_Load(object sender, EventArgs e)
        {
            _screen = Screen.FromPoint(Cursor.Position);
            this.Left = _screen.Bounds.Left;
            this.Top = _screen.Bounds.Top;
            this.Width = _screen.Bounds.Width;
            this.Height = _screen.Bounds.Height;
            this.Focus();
        }

        private void RegionOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_screen.Bounds.Contains(Cursor.Position)) return;

            if (_start == null)
            {
                _start = new MouseEventArgs(e.Button, e.Clicks, Cursor.Position.X, Cursor.Position.Y, e.Delta); // use Cursor.Position since the e.Location is relative to the current screen only
            }
        }

        private void RegionOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_start != null)
            {
                Rectangle rect = GetRectangle(e);
                rect.Offset(-_screen.Bounds.X, -_screen.Bounds.Y); // Drawing is in form coordinates, which is screen-relative, unlike Cursor.Position
                using (Graphics g = this.CreateGraphics())
                using (Pen p = new Pen(this.ForeColor))
                using (Brush b = new SolidBrush(this.ForeColor))
                {
                    g.Clear(this.BackColor);
                    g.FillRectangle(b, rect);
                    g.Flush();
                }
            }
        }

        private void RegionOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != _start.Button)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else if (_start != null)
            {
                SelectedRegion = GetRectangle(e);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
