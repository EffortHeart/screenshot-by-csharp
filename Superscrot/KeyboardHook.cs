using System;
using System.Windows.Forms;

namespace Superscrot
{
    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        /// <summary>
        /// Indicates no modifier keys.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the Alt key.
        /// </summary>
        Alt = 1,

        /// <summary>
        /// Represents the Ctrl key.
        /// </summary>
        Control = 2,

        /// <summary>
        /// Represents the Shift key.
        /// </summary>
        Shift = 4,

        /// <summary>
        /// Represents the Windows key.
        /// </summary>
        Win = 8
    }

    /// <summary>
    /// Provides a global low-level keyboard hook.
    /// </summary>
    public sealed class KeyboardHook : IDisposable
    {
        private int _currentId;

        private Window _window = new Window();

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="Superscrot.KeyboardHook"/> class.
        /// </summary>
        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                    KeyPressed(this, args);
            };
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        /// <summary>
        /// Unregisters the hotkeys that have been registered and releases any resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--)
            {
                NativeMethods.UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="modifier">
        /// The modifiers that are associated with the hot key.
        /// </param>
        /// <param name="key">
        /// The key itself that is associated with the hot key.
        /// </param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId += 1;

            if (!NativeMethods.RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private sealed class Window : System.Windows.Forms.NativeWindow, IDisposable
        {
            public Window()
            {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                this.DestroyHandle();
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == NativeMethods.WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }
        }
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private Keys _key;
        private ModifierKeys _modifier;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        /// <summary>
        /// Gets the key that was pressed.
        /// </summary>
        public Keys Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Gets any modifier keys that have been pressed together with the
        /// pressed <see cref="Key"/>.
        /// </summary>
        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }
    }
}
