using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Superscrot
{
    /// <summary>
    /// Represents the native methods that are shared between assemblies.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        internal const uint SW_HIDE = 0;

        /// <summary>
        /// Activates and displays a window. If the window is minimized or
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window
        /// for the first time.
        /// </summary>
        internal const uint SW_SHOWNORMAL = 1;

        /// <summary>
        /// Posted when the user presses a hot key registered by the
        /// RegisterHotKey function. The message is placed at the top of the
        /// message queue associated with the thread that registered the hot key.
        /// </summary>
        internal const int WM_HOTKEY = 0x0312;

        /// <summary>
        /// Flags used by the <see cref="DwmGetWindowAttribute"/> and
        /// DwmSetWindowAttribute functions to specify window attributes for
        /// non-client rendering.
        /// </summary>
        internal enum DWMWINDOWATTRIBUTE : uint
        {
            /// <summary>
            /// Discovers whether non-client rendering is enabled. The retrieved
            /// value is of type BOOL. TRUE if non-client rendering is enabled;
            /// otherwise, FALSE.
            /// </summary>
            DWMWA_NCRENDERING_ENABLED = 1,

            /// <summary>
            /// Sets the non-client rendering policy. The pvAttribute parameter
            /// points to a value from the DWMNCRENDERINGPOLICY enumeration.
            /// </summary>
            DWMWA_NCRENDERING_POLICY,

            /// <summary>
            /// Enables or forcibly disables DWM transitions. The pvAttribute
            /// parameter points to a value of TRUE to disable transitions or
            /// FALSE to enable transitions.
            /// </summary>
            DWMWA_TRANSITIONS_FORCEDISABLED,

            /// <summary>
            /// Enables content rendered in the non-client area to be visible on
            /// the frame drawn by DWM. The pvAttribute parameter points to a
            /// value of TRUE to enable content rendered in the non-client area
            /// to be visible on the frame; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_ALLOW_NCPAINT,

            /// <summary>
            /// Retrieves the bounds of the caption button area in the
            /// window-relative space. The retrieved value is of type RECT.
            /// </summary>
            DWMWA_CAPTION_BUTTON_BOUNDS,

            /// <summary>
            /// Specifies whether non-client content is right-to-left (RTL)
            /// mirrored. The pvAttribute parameter points to a value of TRUE if
            /// the non-client content is right-to-left (RTL) mirrored;
            /// otherwise, it points to FALSE.
            /// </summary>
            DWMWA_NONCLIENT_RTL_LAYOUT,

            /// <summary>
            /// Forces the window to display an iconic thumbnail or peek
            /// representation (a static bitmap), even if a live or snapshot
            /// representation of the window is available. This value normally
            /// is set during a window's creation and not changed throughout the
            /// window's lifetime. Some scenarios, however, might require the
            /// value to change over time. The pvAttribute parameter points to a
            /// value of TRUE to require a iconic thumbnail or peek
            /// representation; otherwise, it points to FALSE.
            /// </summary>
            DWMWA_FORCE_ICONIC_REPRESENTATION,

            /// <summary>
            /// Sets how Flip3D treats the window. The pvAttribute parameter
            /// points to a value from the DWMFLIP3DWINDOWPOLICY enumeration.
            /// </summary>
            DWMWA_FLIP3D_POLICY,

            /// <summary>
            /// Retrieves the extended frame bounds rectangle in screen space.
            /// The retrieved value is of type RECT.
            /// </summary>
            DWMWA_EXTENDED_FRAME_BOUNDS,

            /// <summary>
            /// The window will provide a bitmap for use by DWM as an iconic
            /// thumbnail or peek representation (a static bitmap) for the
            /// window. DWMWA_HAS_ICONIC_BITMAP can be specified with
            /// DWMWA_FORCE_ICONIC_REPRESENTATION. DWMWA_HAS_ICONIC_BITMAP
            /// normally is set during a window's creation and not changed
            /// throughout the window's lifetime. Some scenarios, however, might
            /// require the value to change over time. The pvAttribute parameter
            /// points to a value of TRUE to inform DWM that the window will
            /// provide an iconic thumbnail or peek representation; otherwise,
            /// it points to FALSE.
            /// </summary>
            DWMWA_HAS_ICONIC_BITMAP,

            /// <summary>
            /// Do not show peek preview for the window. The peek view shows a
            /// full-sized preview of the window when the mouse hovers over the
            /// window's thumbnail in the taskbar. If this attribute is set,
            /// hovering the mouse pointer over the window's thumbnail dismisses
            /// peek (in case another window in the group has a peek preview
            /// showing). The pvAttribute parameter points to a value of TRUE to
            /// prevent peek functionality or FALSE to allow it.
            /// </summary>
            DWMWA_DISALLOW_PEEK,

            /// <summary>
            /// Prevents a window from fading to a glass sheet when peek is
            /// invoked. The pvAttribute parameter points to a value of TRUE to
            /// prevent the window from fading during another window's peek or
            /// FALSE for normal behavior.
            /// </summary>
            DWMWA_EXCLUDED_FROM_PEEK,

            /// <summary>
            /// Cloaks the window such that it is not visible to the user. The
            /// window is still composed by DWM.
            /// </summary>
            DWMWA_CLOAK,

            /// <summary>
            /// If the window is cloaked, provides one of the following values
            /// explaining why:
            /// </summary>
            DWMWA_CLOAKED,

            /// <summary>
            /// Freeze the window's thumbnail image with its current visuals. Do
            /// no further live updates on the thumbnail image to match the
            /// window's contents.
            /// </summary>
            DWMWA_FREEZE_REPRESENTATION,

            /// <summary>
            /// The maximum recognized DWMWINDOWATTRIBUTE value, used for
            /// validation purposes.
            /// </summary>
            DWMWA_LAST
        }

        /// <summary>
        /// The current show state of the window.
        /// </summary>
        internal enum SHOWCMD : uint
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            SW_HIDE = 0,

            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            SW_MAXIMIZE = 3,

            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the z-order.
            /// </summary>
            SW_MINIMIZE = 6,

            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and
            /// position. An application should specify this flag when restoring
            /// a minimized window.
            /// </summary>
            SW_RESTORE = 9,

            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            SW_SHOW = 5,

            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            SW_SHOWMAXIMIZED = 3,

            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            SW_SHOWMINIMIZED = 2,

            /// <summary>
            /// Displays the window as a minimized window. This value is similar
            /// to <see cref="SW_SHOWMINIMIZED"/>, except the window is not activated.
            /// </summary>
            SW_SHOWMINNOACTIVE = 7,

            /// <summary>
            /// Displays the window in its current size and position. This value
            /// is similar to <see cref="SW_SHOW"/>, except the window is not activated.
            /// </summary>
            SW_SHOWNA = 8,

            /// <summary>
            /// Displays a window in its most recent size and position. This
            /// value is similar to <see cref="SW_SHOWNORMAL"/>, except the
            /// window is not activated.
            /// </summary>
            SW_SHOWNOACTIVATE = 4,

            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and
            /// position. An application should specify this flag when
            /// displaying the window for the first time.
            /// </summary>
            SW_SHOWNORMAL = 1
        }

        /// <summary>
        /// Retrieves the current value of a specified attribute applied to a window.
        /// </summary>
        /// <param name="hwnd">
        /// The handle to the window from which the attribute data is retrieved.
        /// </param>
        /// <param name="dwAttribute">
        /// The attribute to retrieve, specified as a <see
        /// cref="DWMWINDOWATTRIBUTE"/> value.
        /// </param>
        /// <param name="pvAttribute">
        /// A pointer to a value that, when this function returns successfully,
        /// receives the current value of the attribute. The type of the
        /// retrieved value depends on the value of the <paramref
        /// name="dwAttribute"/> parameter.
        /// </param>
        /// <param name="cbAttribute">
        /// The size of the <see cref="DWMWINDOWATTRIBUTE"/> value being
        /// retrieved. The size is dependent on the type of the pvAttribute parameter.
        /// </param>
        /// <returns>
        /// If this function succeeds, it returns S_OK. Otherwise, it returns an
        /// HRESULT error code.
        /// </returns>
        [DllImport("dwmapi.dll", SetLastError = false)]
        internal static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out RECT pvAttribute, uint cbAttribute);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which
        /// the user is currently working). The system assigns a slightly higher
        /// priority to the thread that creates the foreground window than it
        /// does to other threads.
        /// </summary>
        /// <returns>
        /// The return value is a handle to the foreground window. The
        /// foreground window can be NULL in certain circumstances, such as when
        /// a window is losing activation.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves the handle to the window that currently has the clipboard open.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the
        /// window that has the clipboard open. If no window has the clipboard
        /// open, the return value is <see cref="IntPtr.Zero"/>.
        /// </returns>
        /// <remarks>
        /// If an application or DLL specifies a <c>null</c> window handle when
        /// calling the <c>OpenClipboard</c> function, the clipboard is opened
        /// but is not associated with a window. In such a case,
        /// <c>GetOpenClipboardWindow</c> returns <c>null</c>.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr GetOpenClipboardWindow();

        /// <summary>
        /// Retrieves information about the specified window.
        /// </summary>
        /// <param name="hwnd">
        /// A handle to the window whose information is to be retrieved.
        /// </param>
        /// <param name="pwi">
        /// A pointer to a <see cref="WINDOWINFO"/> structure to receive the
        /// information. Note that you must set the <c>cbSize</c> member to
        /// sizeof( <see cref="WINDOWINFO"/>) before calling this function.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. If the
        /// function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized
        /// positions of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpwndpl">
        /// A pointer to the <see cref="WINDOWPLACEMENT"/> structure that
        /// receives the show state and position information. Before calling
        /// GetWindowPlacement, set the length member to sizeof( <see
        /// cref="WINDOWPLACEMENT"/>). GetWindowPlacement fails if <paramref
        /// name="lpwndpl"/>-&gt;length is not set correctly.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. If the
        /// function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// The flags member of <see cref="WINDOWPLACEMENT"/> retrieved by this
        /// function is always zero. If the window identified by the <paramref
        /// name="hWnd"/> parameter is maximized, the showCmd member is
        /// SW_SHOWMAXIMIZED. If the window is minimized, showCmd is
        /// SW_SHOWMINIMIZED. Otherwise, it is <see cref="SW_SHOWNORMAL"/>.
        /// 
        /// The length member of <see cref="WINDOWPLACEMENT"/> must be set to
        /// sizeof( <see cref="WINDOWPLACEMENT"/>). If this member is not set
        /// correctly, the function returns FALSE. For additional remarks on the
        /// proper use of window placement coordinates, see <see cref="WINDOWPLACEMENT"/>.
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified
        /// window. The dimensions are given in screen coordinates that are
        /// relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="lpRect">
        /// A pointer to a RECT structure that receives the screen coordinates
        /// of the upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. If the
        /// function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one)
        /// into a buffer. If the specified window is a control, the text of the
        /// control is copied. However, GetWindowText cannot retrieve the text
        /// of a control in another application.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window or control containing the text.
        /// </param>
        /// <param name="lpString">
        /// The buffer that will receive the text. If the string is as long or
        /// longer than the buffer, the string is truncated and terminated with
        /// a null character.
        /// </param>
        /// <param name="nMaxCount">
        /// The maximum number of characters to copy to the buffer, including
        /// the null character. If the text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in
        /// characters, of the copied string, not including the terminating null
        /// character. If the window has no title bar or text, if the title bar
        /// is empty, or if the window or control handle is invalid, the return
        /// value is zero. To get extended error information, call GetLastError.
        /// This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Retrieves the length, in characters, of the specified window's title
        /// bar text (if the window has a title bar). If the specified window is
        /// a control, the function retrieves the length of the text within the
        /// control. However, GetWindowTextLength cannot retrieve the length of
        /// the text of an edit control in another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control.</param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in
        /// characters, of the text. Under certain conditions, this value may
        /// actually be greater than the length of the text. For more
        /// information, see the following Remarks section. If the window has no
        /// text, the return value is zero. To get extended error information,
        /// call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified
        /// window and, optionally, the identifier of the process that created
        /// the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">
        /// A pointer to a variable that receives the process identifier. If
        /// this parameter is not <c>null</c>, GetWindowThreadProcessId copies
        /// the identifier of the process to the variable; otherwise, it does not.
        /// </param>
        /// <returns>
        /// The return value is the identifier of the thread that created the window.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint lpdwProcessId);

        /// <summary>
        /// Defines a system-wide hot key.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window that will receive WM_HOTKEY messages
        /// generated by the hot key. If this parameter is NULL, WM_HOTKEY
        /// messages are posted to the message queue of the calling thread and
        /// must be processed in the message loop.
        /// </param>
        /// <param name="id">
        /// The identifier of the hot key. If the hWnd parameter is NULL, then
        /// the hot key is associated with the current thread rather than with a
        /// particular window. If a hot key already exists with the same hWnd
        /// and id parameters, see Remarks for the action taken.
        /// </param>
        /// <param name="fsModifiers">
        /// The keys that must be pressed in combination with the key specified
        /// by the uVirtKey parameter in order to generate the WM_HOTKEY message.
        /// </param>
        /// <param name="vk">The virtual-key code of the hot key.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. If the
        /// function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

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

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">Controls how the window is to be shown.</param>
        /// <returns>
        /// If the window was previously visible, the return value is nonzero.
        /// If the window was previously hidden, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        /// <summary>
        /// Converts a numeric value into a string that represents the number
        /// expressed as a size value in bytes, kilobytes, megabytes, or
        /// gigabytes, depending on the size.
        /// </summary>
        /// <param name="fileSize">The numeric value to be converted.</param>
        /// <param name="buffer">
        /// A pointer to a buffer that, when this function returns successfully,
        /// receives the converted number.
        /// </param>
        /// <param name="bufferSize">
        /// The size of the buffer pointed to by pszBuf, in characters.
        /// </param>
        /// <returns>
        /// Returns a pointer to the converted string, or NULL if the conversion fails.
        /// </returns>
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        internal static extern long StrFormatByteSize(long fileSize, System.Text.StringBuilder buffer, int bufferSize);

        /// <summary>
        /// Frees a hot key previously registered by the calling thread.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window associated with the hot key to be freed. This
        /// parameter should be NULL if the hot key is not associated with a window.
        /// </param>
        /// <param name="id">The identifier of the hot key to be freed.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. If the
        /// function fails, the return value is zero. To get extended error
        /// information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// The POINT structure defines the x- and y- coordinates of a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            /// <summary>
            /// The x-coordinate of the point.
            /// </summary>
            public int x;

            /// <summary>
            /// The y-coordinate of the point.
            /// </summary>
            public int y;

            /// <summary>
            /// Converts the specified <see cref="POINT"/> instance to a new
            /// instance of the <see cref="System.Drawing.Point"/> class.
            /// </summary>
            /// <param name="value">
            /// The <see cref="POINT"/> instance to convert from.
            /// </param>
            /// <returns>A new <see cref="System.Drawing.Point"/> instance.</returns>
            public static implicit operator System.Drawing.Point(POINT value)
            {
                return new System.Drawing.Point(value.x, value.y);
            }

            /// <summary>
            /// Converts the specified <see cref="System.Drawing.Point"/>
            /// instance to a new instance of the <see cref="POINT"/> class.
            /// </summary>
            /// <param name="value">
            /// The <see cref="System.Drawing.Point"/> instance to convert from.
            /// </param>
            /// <returns>A new <see cref="POINT"/> instance.</returns>
            public static implicit operator POINT(System.Drawing.Point value)
            {
                return new POINT { x = value.X, y = value.Y };
            }
        }

        /// <summary>
        /// The RECT structure defines the coordinates of the upper-left and
        /// lower-right corners of a rectangle.
        /// </summary>
        /// <remarks>
        /// By convention, the right and bottom edges of the rectangle are
        /// normally considered exclusive. In other words, the pixel whose
        /// coordinates are (right, bottom) lies immediately outside of the
        /// rectangle. For example, when RECT is passed to the FillRect
        /// function, the rectangle is filled up to, but not including, the
        /// right column and bottom row of pixels.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            /// <summary>
            /// The x-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Left;

            /// <summary>
            /// The y-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Top;

            /// <summary>
            /// The x-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Right;

            /// <summary>
            /// The y-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Bottom;
        }

        /// <summary>
        /// Contains window information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWINFO
        {
            /// <summary>
            /// The size of the structure, in bytes. The caller must set this
            /// member to sizeof( <see cref="WINDOWINFO"/>).
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// The coordinates of the window.
            /// </summary>
            public RECT rcWindow;

            /// <summary>
            /// The coordinates of the client area.
            /// </summary>
            public RECT rcClient;

            /// <summary>
            /// The window styles. For a table of window styles, see Window Styles.
            /// </summary>
            public uint dwStyle;

            /// <summary>
            /// The extended window styles. For a table of extended window
            /// styles, see Extended Window Styles.
            /// </summary>
            public uint dwExStyle;

            /// <summary>
            /// The window status. If this member is WS_ACTIVECAPTION (0x0001),
            /// the window is active. Otherwise, this member is zero.
            /// </summary>
            public uint dwWindowStatus;

            /// <summary>
            /// The width of the window border, in pixels.
            /// </summary>
            public uint cxWindowBorders;

            /// <summary>
            /// The height of the window border, in pixels.
            /// </summary>
            public uint cyWindowBorders;

            /// <summary>
            /// The window class atom (see RegisterClass).
            /// </summary>
            public ushort atomWindowType;

            /// <summary>
            /// The Windows version of the application that created the window.
            /// </summary>
            public ushort wCreatorVersion;
        }

        /// <summary>
        /// Contains information about the placement of a window on the screen.
        /// </summary>
        /// <remarks>
        /// If the window is a top-level window that does not have the
        /// WS_EX_TOOLWINDOW window style, then the coordinates represented by
        /// the following members are in workspace
        /// coordinates: ptMinPosition, ptMaxPosition, and rcNormalPosition.
        ///              Otherwise, these members are in screen coordinates.
        /// 
        /// Workspace coordinates differ from screen coordinates in that they
        /// take the locations and sizes of application toolbars (including the
        /// taskbar) into account. Workspace coordinate (0,0) is the upper-left
        ///          corner of the workspace area, the area of the screen not
        /// being used by application toolbars.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            /// <summary>
            /// The length of the structure, in bytes. Before calling the <see
            /// cref="GetWindowPlacement"/> or SetWindowPlacement functions, set
            /// this member to sizeof( <see cref="WINDOWPLACEMENT"/>).
            /// GetWindowPlacement and SetWindowPlacement fail if this member is
            /// not set correctly.
            /// </summary>
            public uint length;

            /// <summary>
            /// The flags that control the position of the minimized window and
            /// the method by which the window is restored. This member can be
            /// one or more of the following values.
            /// </summary>
            public uint flags;

            /// <summary>
            /// The current show state of the window. This member can be one of
            /// the following values.
            /// </summary>
            public SHOWCMD showCmd;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the
            /// window is minimized.
            /// </summary>
            public POINT ptMinPosition;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the
            /// window is maximized.
            /// </summary>
            public POINT ptMaxPosition;

            /// <summary>
            /// The window's coordinates when the window is in the restored position.
            /// </summary>
            public RECT rcNormalPosition;
        }
    }
}
