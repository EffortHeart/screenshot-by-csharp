using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Superscrot.Controls
{
    /// <summary>
    /// Represents a Windows trackbar control with labels indicating the range
    /// and current value.
    /// </summary>
    [DefaultEvent("ValueChanged")]
    public partial class Slider : UserControl
    {
        private string format;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slider"/> control.
        /// </summary>
        public Slider()
        {
            InitializeComponent();
            Minimum = 0;
            Maximum = 100;
            TickFrequency = 10;
            SmallChange = 1;
            LargeChange = 5;
        }

        /// <summary>
        /// Occurs when the value of the <see cref="Value"/> property has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs whenever the Value property changes.")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets the format string used to format the value.
        /// </summary>
        [Category("Appearance")]
        [Description("The standard or custom numeric format string used to format the value.")]
        public string Format
        {
            get { return format; }
            set
            {
                if (value != format)
                {
                    format = value;
                    UpdateDisplay();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value added to or subtracted from the <see
        /// cref="Value"/> property when the scroll box is moved a large distance.
        /// </summary>
        [Category("Behaviour")]
        [Description("The number of positions the sliders moves in response to mouse clicks or the PAGE UP and PAGE DOWN keys.")]
        [DefaultValue(5)]
        public int LargeChange
        {
            get { return trackBar.LargeChange; }
            set { trackBar.LargeChange = value; }
        }

        /// <summary>
        /// Gets or sets the upper limit of the range the control is working with.
        /// </summary>
        [Category("Behaviour")]
        [Description("The maximum value for the position of the slider.")]
        [DefaultValue(100)]
        public int Maximum
        {
            get { return trackBar.Maximum; }
            set { trackBar.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with the upper limit of the range
        /// the control is working with.
        /// </summary>
        [Category("Appearance")]
        [Description("The text displayed on the right side of the control.")]
        public string MaximumText
        {
            get { return rightLabel.Text; }
            set { rightLabel.Text = value; }
        }

        /// <summary>
        /// Gets or sets the lower limit of the range the control is working with.
        /// </summary>
        [Category("Behaviour")]
        [Description("The minimum value for the position of the slider.")]
        [DefaultValue(0)]
        public int Minimum
        {
            get { return trackBar.Minimum; }
            set { trackBar.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with the lower limit of the range
        /// the control is working with.
        /// </summary>
        [Category("Appearance")]
        [Description("The text displayed on the left side of the control.")]
        public string MinimumText
        {
            get { return leftLabel.Text; }
            set { leftLabel.Text = value; }
        }

        /// <summary>
        /// Gets or sets the value added to or subtracted from the <see
        /// cref="Value"/> property when the scroll box is moved a small distance.
        /// </summary>
        [Category("Behaviour")]
        [Description("The number of positions the sliders moves in response to keyboard input.")]
        [DefaultValue(1)]
        public int SmallChange
        {
            get { return trackBar.SmallChange; }
            set { trackBar.SmallChange = value; }
        }

        /// <summary>
        /// Gets or sets a value that specifies the delta between ticks drawn on
        /// the control.
        /// </summary>
        [Category("Behaviour")]
        [Description("The number of positions between tick marks.")]
        [DefaultValue(10)]
        public int TickFrequency
        {
            get { return trackBar.TickFrequency; }
            set { trackBar.TickFrequency = value; }
        }

        /// <summary>
        /// Gets or sets a numeric value that is represented by the current
        /// position of the scroll box on the track bar.
        /// </summary>
        [Category("Behaviour")]
        [Description("The value of the slider.")]
        [DefaultValue(0)]
        public int Value
        {
            get { return trackBar.Value; }
            set
            {
                if (value != trackBar.Value)
                {
                    trackBar.Value = value;
                    OnValueChanged();
                }
            }
        }

        /// <summary>
        /// Repositions the containing controls when the labels are resized.
        /// </summary>
        protected virtual void DoResize()
        {
            trackBar.Left = leftLabel.Right + leftLabel.Margin.Right;
            trackBar.Width = Width - leftLabel.Width - rightLabel.Width - trackBar.Margin.Horizontal;
            valueLabel.Width = Width;
        }

        /// <summary>
        /// Occurs when the control is resized.
        /// </summary>
        /// <param name="e">
        /// An <see cref="EventArgs"/> containg the event data.
        /// </param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DoResize();
        }

        /// <summary>
        /// Raises the <see cref="ValueChanged"/> event.
        /// </summary>
        protected virtual void OnValueChanged()
        {
            UpdateDisplay();

            var handler = ValueChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void leftLabel_Resize(object sender, EventArgs e)
        {
            DoResize();
        }

        private void rightLabel_Resize(object sender, EventArgs e)
        {
            DoResize();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }

        private void UpdateDisplay()
        {
            valueLabel.Text = Value.ToString(Format);
        }
    }
}
