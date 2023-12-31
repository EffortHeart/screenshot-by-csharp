using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Superscrot.Controls
{
    /// <summary>
    /// Represents a Windows split button that allows the user to select a color.
    /// </summary>
    [DefaultEvent("ColorChanged")]
    public class ColorPickerButton : System.Windows.Forms.Button
    {
        private Color color = Color.Transparent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerButton"/> control.
        /// </summary>
        public ColorPickerButton()
        {
            base.Text = string.Empty;
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Color"/> property has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs whenever the Color property changes.")]
        public event EventHandler ColorChanged;

        /// <summary>
        /// Gets or sets the selected <see cref="System.Drawing.Color"/>.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Transparent")]
        [Description("The color associated with the control.")]
        public Color Color
        {
            get { return color; }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnColorChanged();
                }
            }
        }

        /// <summary>
        /// The Text property is not supported by the ColorPickerButton.
        /// </summary>
        [Browsable(false)]
        [DefaultValue("")]
        [Obsolete("The Text property is not supported by the ColorPickerButton", true)]
        public new string Text
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets the internal spacing, in pixels, of the contents of a control.
        /// </summary>
        protected override Padding DefaultPadding
        {
            get
            {
                return new Padding(6, 4, 6, 4);
            }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(55, 23);
            }
        }

        /// <summary>
        /// Shows a color dialog and raises the Click event.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> that contains the event data.
        /// </param>
        protected override void OnClick(EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = Color;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Color = dialog.Color;
                }
            }

            base.OnClick(e);
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged"/> event.
        /// </summary>
        protected virtual void OnColorChanged()
        {
            var handler = ColorChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="pevent">
        /// A <see cref="PaintEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            var rect = new Rectangle()
            {
                X = pevent.ClipRectangle.X + Padding.Left,
                Y = pevent.ClipRectangle.Y + Padding.Top,
                Width = pevent.ClipRectangle.Width - Padding.Horizontal,
                Height = pevent.ClipRectangle.Height - Padding.Vertical
            };

            if (Enabled)
            {
                var brush = new SolidBrush(Color);

                pevent.Graphics.FillRectangle(brush, rect);
                ControlPaint.DrawBorder(pevent.Graphics, rect, SystemColors.ControlDark,
                    ButtonBorderStyle.Solid);
            }
        }
    }
}
