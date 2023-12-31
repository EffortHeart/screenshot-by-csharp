using System;
using System.Runtime.InteropServices;

namespace Superscrot.Controls
{
    internal static class NativeMethods
    {
        internal const int BCM_FIRST = 0x1600;

        /// <summary>
        /// Sets the text of the note associated with a command link button.
        /// </summary>
        internal const int BCM_SETNOTE = (BCM_FIRST + 0x0009);

        /// <summary>
        /// Creates a command link button that behaves like a BS_PUSHBUTTON
        /// style button, but the command link button has a green arrow on the
        /// left pointing to the button text. A caption for the button text can
        /// be set by sending the BCM_SETNOTE message to the button.
        /// </summary>
        internal const int BS_COMMANDLINK = 0x000E;

        /// <summary>
        /// Creates a command link button that behaves like a BS_PUSHBUTTON
        /// style button. If the button is in a dialog box, the user can select
        /// the command link button by pressing the ENTER key, even when the
        /// command link button does not have the input focus. This style is
        /// useful for enabling the user to quickly select the most likely
        /// (default) option.
        /// </summary>
        internal const int BS_DEFCOMMANDLINK = 0x000F;

        internal const int ECM_FIRST = 0x1500;

        /// <summary>
        /// Sets the textual cue, or tip, that is displayed by the edit control
        /// to prompt the user for information.
        /// </summary>
        internal const int EM_SETCUEBANNER = (ECM_FIRST + 0x0001);

        /// <summary>
        /// Sends the specified message to a window or windows. The SendMessage
        /// function calls the window procedure for the specified window and
        /// does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose window procedure will receive the
        /// message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the
        /// message is sent to all top-level windows in the system, including
        /// disabled or invisible unowned windows, overlapped windows, and
        /// pop-up windows; but the message is not sent to child windows.
        /// Message sending is subject to UIPI. The thread of a process can send
        /// messages only to message queues of threads in processes of lesser or
        /// equal integrity level.
        /// </param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        /// The return value specifies the result of the message processing; it
        /// depends on the message sent.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    }
}
