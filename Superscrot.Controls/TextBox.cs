using System;
using System.ComponentModel;

namespace Superscrot.Controls
{
    /// <summary>
    /// Represents a Windows text box control that has been extended with the
    /// ability to display placeholder text.
    /// </summary>
    public class TextBox : System.Windows.Forms.TextBox
    {
        private string placeholder = string.Empty;

        /// <summary>
        /// Occurs when the value of the <see cref="Placeholder"/> property has changed.
        /// </summary>
        [Category("Property Changed")]
        [Description("Occurs whenever the Placeholder property changes.")]
        public event EventHandler PlaceholderChanged;

        /// <summary>
        /// Gets or sets the placeholder text associated with the control.
        /// </summary>
        [Category("Appearance")]
        [Description("The placeholder text associated with the control.")]
        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                if (value != placeholder)
                {
                    placeholder = value;
                    OnPlaceholderChanged();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="PlaceholderChanged"/> event.
        /// </summary>
        protected virtual void OnPlaceholderChanged()
        {
            SetCue(Placeholder);

            var handler = PlaceholderChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void SetCue(string cue)
        {
            NativeMethods.SendMessage(Handle, NativeMethods.EM_SETCUEBANNER, IntPtr.Zero, cue);
        }
    }
}
