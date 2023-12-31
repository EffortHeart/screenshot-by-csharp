using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;
using System.Drawing;
using System.Runtime.InteropServices;
using SlimDX;

public class DwmCapture
{
    private Direct3DEx _d3dEx;
    private DeviceEx _deviceEx;
    private Surface _sharedSurface;
    private Surface _renderTarget;
    private Surface _systemMemorySurface;

    private IntPtr _captureHwnd;
    private long _adapterLuid;

    public DwmCapture(IntPtr hWnd)
    {
        _captureHwnd = hWnd;

        #region Initialise the Direct3D device

        int adapterOrdinal = 0;

        _d3dEx = new Direct3DEx();
        _adapterLuid = _d3dEx.GetAdapterLuid(adapterOrdinal);

        var presentParams = new PresentParameters
        {
            PresentFlags = PresentFlags.LockableBackBuffer,
            Windowed = true,
            BackBufferFormat = Format.A8R8G8B8,
            SwapEffect = SwapEffect.Flip
        };

        _deviceEx = new DeviceEx(_d3dEx, adapterOrdinal, DeviceType.Hardware, _captureHwnd, SlimDX.Direct3D9.CreateFlags.Multithreaded | SlimDX.Direct3D9.CreateFlags.SoftwareVertexProcessing, presentParams);

        #endregion

        #region Setup the shared surface (using DWM)

        uint format = 0;
        IntPtr pSharedHandle = IntPtr.Zero;
        int hr = NativeMethods.GetSharedSurface(_captureHwnd, _adapterLuid, 0, 0, ref format, out pSharedHandle, 0);
        NativeMethods.UpdateWindowShared(_captureHwnd, 0, 0, 0, IntPtr.Zero, IntPtr.Zero);

        RECT winRect;
        NativeMethods.GetWindowRect(_captureHwnd, out winRect);

        Size size = new Size(winRect.Right - winRect.Left, winRect.Bottom - winRect.Top);

        /* Hack because SlimDX does not let you specify a shared handle for creating shared resources we
         * have to create an IDirect3DDevice9 reference to the device instead */
        IDirect3DDevice9 devEx = (IDirect3DDevice9)Marshal.GetObjectForIUnknown(_deviceEx.ComPointer);
        IntPtr pTexture;
        devEx.CreateTexture((int)size.Width, (int)size.Height, 1, 1, format, 0, out pTexture, ref pSharedHandle);
        Texture texture = Texture.FromPointer(pTexture);

        _sharedSurface = texture.GetSurfaceLevel(0);

        _renderTarget = Surface.CreateRenderTarget(_deviceEx, (int)size.Width, (int)size.Height, Format.X8R8G8B8,
                                                    MultisampleType.None, 0, false);

        _deviceEx.SetRenderTarget(0, _renderTarget);

        Surface.FromSurface(_renderTarget, _sharedSurface, Filter.None, 0);

        _systemMemorySurface = Surface.CreateOffscreenPlain(_deviceEx, (int)size.Width, (int)size.Height,
                                                             Format.X8R8G8B8, Pool.SystemMemory);

        #endregion
    }

    /// <summary>
    /// Gets the current surface bitmap for the entire client window
    /// </summary>
    /// <returns>A new Bitmap object (must be Disposed by caller)</returns>
    public Bitmap GetSurfaceBitmap()
    {
        Rectangle clientRect = NativeMethods.GetClientRect(_captureHwnd);
        return GetSurfaceRegion(clientRect);
    }

    /// <summary>
    /// Gets the specified region (identified in client coordinates) from the current surface
    /// </summary>
    /// <param name="region">The region (in client coordinates, e.g. <see cref="NativeMethods.GetClientRect"/> is the entire window content).</param>
    /// <returns>A new Bitmap object (must be Disposed by caller)</returns>
    public Bitmap GetSurfaceRegion(Rectangle region)
    {
        if (_captureHwnd == IntPtr.Zero)
            return null;

        #region Create a copy of the surface image

        NativeMethods.UpdateWindowShared(_captureHwnd, 0, 0, 0, IntPtr.Zero, IntPtr.Zero);
        Surface.FromSurface(_renderTarget, _sharedSurface, Filter.None, 0);

        _deviceEx.GetRenderTargetData(_renderTarget, _systemMemorySurface);

        #endregion

        #region Put the data into a Bitmap

        // Note: during my tests I found that the captured area includes space for the window chrome
        //       but filled in black.

        // Take into consideration the window chrome
        Size chrome = NativeMethods.GetWindowChromeSize(_captureHwnd);
        region.X += chrome.Width;
        region.Y += chrome.Height;

        // We need to convert Width and Height to x2, and y2 respectively, because
        // Surface.ToStream(...) expects the rectangle to have x1,y1,x2,y2 not x1,y1,width,height
        // (SlimDX version 2.0.6.40)
        region.Width += region.X;
        region.Height += region.Y;
        DataStream stream = Surface.ToStream(_systemMemorySurface, ImageFileFormat.Bmp, region);

        Bitmap surfaceBitmap = (Bitmap)Image.FromStream(stream);

        stream.Close();
        stream.Dispose();

        #endregion

        return surfaceBitmap;
    }
}

#region Native Win32 Interop
/// <summary>
/// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
/// </summary>
[Serializable, StructLayout(LayoutKind.Sequential)]
internal struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public RECT(int left, int top, int right, int bottom)
    {
        this.Left = left;
        this.Top = top;
        this.Right = right;
        this.Bottom = bottom;
    }

    public Rectangle AsRectangle
    {
        get
        {
            return new Rectangle(this.Left, this.Top, this.Right - this.Left, this.Bottom - this.Top);
        }
    }

    public static RECT FromXYWH(int x, int y, int width, int height)
    {
        return new RECT(x, y, x + width, y + height);
    }

    public static RECT FromRectangle(Rectangle rect)
    {
        return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
    }
}

[System.Security.SuppressUnmanagedCodeSecurity()]
internal sealed class NativeMethods
{
    #region user32

    [DllImport("user32.dll")]
    internal static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    /// <summary>
    /// The FindWindow function retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search. To search child windows, beginning with a specified child window, use the FindWindowEx function.
    /// </summary>
    /// <param name="lpClassName">Window class name, can be null</param>
    /// <param name="lpWindowName">Window caption, can be null</param>
    /// <returns></returns>
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    #endregion

    #region dwmapi

    [PreserveSig]
    [DllImport("dwmapi.dll", EntryPoint = "#101")]
    public static extern int UpdateWindowShared(IntPtr hWnd, int one, int two, int three, IntPtr hMonitor, IntPtr unknown);

    [PreserveSig]
    [DllImport("dwmapi.dll", EntryPoint = "#100")]
    public static extern int GetSharedSurface(IntPtr hWnd, Int64 adapterLuid, uint one, uint two, [In, Out]ref uint pD3DFormat, [Out]out IntPtr pSharedHandle, UInt64 unknown);

    #endregion

    #region Helpers

    /// <summary>
    /// Get a windows client rectangle in a .NET structure
    /// </summary>
    /// <param name="hwnd">The window handle to look up</param>
    /// <returns>The rectangle</returns>
    internal static Rectangle GetClientRect(IntPtr hwnd)
    {
        RECT rect = new RECT();
        GetClientRect(hwnd, out rect);
        return rect.AsRectangle;
    }

    /// <summary>
    /// Get a windows rectangle in a .NET structure
    /// </summary>
    /// <param name="hwnd">The window handle to look up</param>
    /// <returns>The rectangle</returns>
    internal static Rectangle GetWindowRect(IntPtr hwnd)
    {
        RECT rect = new RECT();
        GetWindowRect(hwnd, out rect);
        return rect.AsRectangle;
    }

    internal static Rectangle GetAbsoluteClientRect(IntPtr hWnd)
    {
        Rectangle windowRect = NativeMethods.GetWindowRect(hWnd);
        Rectangle clientRect = NativeMethods.GetClientRect(hWnd);

        // This gives us the width of the left, right and bottom chrome - we can then determine the top height
        int chromeWidth = (int)((windowRect.Width - clientRect.Width) / 2);

        return new Rectangle(new Point(windowRect.X + chromeWidth, windowRect.Y + (windowRect.Height - clientRect.Height - chromeWidth)), clientRect.Size);
    }

    /// <summary>
    /// <para>Returns the size of the window chrome</para>
    /// <para>Returns a Size object where Size.Width represents the width of the chrome on left and 
    /// right and also the height of the bottom chrome, and Size.Height represents the height of the title bar.</para>
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns>
    /// </returns>
    internal static Size GetWindowChromeSize(IntPtr hWnd)
    {
        Rectangle windowRect = NativeMethods.GetWindowRect(hWnd);
        Rectangle clientRect = NativeMethods.GetClientRect(hWnd);

        // This gives us the width of the left, right and bottom crome - we can then determine the top height
        int chromeWidth = (int)((windowRect.Width - clientRect.Width) / 2);
        int chromeHeight = (int)(windowRect.Height - clientRect.Height - chromeWidth);

        return new Size(chromeWidth, chromeHeight);
    }

    #endregion
}

#endregion