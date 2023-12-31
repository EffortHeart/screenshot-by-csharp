using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Superscrot.Controls
{
    /// <summary>
    /// Represents a Windows command link button.
    /// </summary>
    public class CommandLink : Button
    {
        private string description = string.Empty;
        private int style = NativeMethods.BS_COMMANDLINK;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLink"/> control.
        /// </summary>
        public CommandLink()
            : base()
        {
            base.FlatStyle = System.Windows.Forms.FlatStyle.System;
        }

        /// <summary>
        /// Gets or sets the text of the note associated with the control.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("Sets the text of the note associated with the control.")]
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    UpdateDescription();
                }
            }
        }

        /// <summary>
        /// Gets or sets the flat style appearance of the button control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DefaultValue(typeof(FlatStyle), "System")]
        public new FlatStyle FlatStyle
        {
            get { return System.Windows.Forms.FlatStyle.System; }
            set { }
        }

        /// <summary>
        /// Gets a CreateParams on the base class when creating a window. Sets
        /// the CommandLink button style.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var value = base.CreateParams;
                value.Style |= style;
                return value;
            }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(150, 41); // 58 with a one-line description
            }
        }

        /// <summary>
        /// Notifies the Button whether it is the default button so that it can
        /// adjust its appearance accordingly.
        /// </summary>
        /// <param name="value">
        /// <c>true</c> if the button is to have the appearance of the default
        /// button; otherwise, <c>false</c>.
        /// </param>
        public override void NotifyDefault(bool value)
        {
            style = value ? NativeMethods.BS_DEFCOMMANDLINK : NativeMethods.BS_COMMANDLINK;
            base.NotifyDefault(value);
        }

        /// <summary>
        /// Updates the command link's note.
        /// </summary>
        protected void UpdateDescription()
        {
            RecreateHandle();
            NativeMethods.SendMessage(Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, Description);
        }
    }
}
