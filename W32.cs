using System;
using System.Runtime.InteropServices;
using System.Text;

namespace API
{
    internal class W32
    {
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_SENDWININICHANGE = 2;
        public const int SPIF_UPDATEINIFILE = 1;

        public const int SRCCOPY = 13369376;

        public const int WM_GETICON = 127;

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
          IntPtr hdcDest,
          int xDest,
          int yDest,
          int wDest,
          int hDest,
          IntPtr hdcSource,
          int xSrc,
          int ySrc,
          int RasterOp);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hDc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteObject(IntPtr hDc);
        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref W32.BbStruct blurBehind);
        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref W32.Margins pMarInset);
        [DllImport("dwmapi.dll", EntryPoint = "#127", PreserveSig = false)]
        public static extern void DwmGetColorizationParameters(out W32.DWM_COLORIZATION_PARAMS parameters);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);
        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(
          ref W32.DWM_COLORIZATION_PARAMS parameters, long uUnknown);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(
          IntPtr hwnd,
          int attr,
          ref int attrValue,
          int attrSize);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        [DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventid, int flags, IntPtr item1, IntPtr item2);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(
          IntPtr parentHandle,
          W32.EnumWindowsProc lpEnumFunc,
          IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(
          IntPtr parentHandle,
          IntPtr childAfter,
          string className,
          IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern uint GetClassLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(
          IntPtr hWnd,
          IntPtr hrgnClip,
          W32.DeviceContextValues flags);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int abc);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(int ptr);
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(
          IntPtr hWnd,
          [In] ref W32.RECT lprcUpdate,
          IntPtr hrgnUpdate,
          W32.RedrawWindowFlags flags);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(
          IntPtr hWnd,
          IntPtr lprcUpdate,
          IntPtr hrgnUpdate,
          W32.RedrawWindowFlags flags);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(
          IntPtr hWnd,
          uint Msg,
          int wParam,
          IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
          IntPtr windowHandle,
          uint Msg,
          IntPtr wParam,
          IntPtr lParam,
          W32.SendMessageTimeoutFlags flags,
          uint timeout,
          out IntPtr result);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(
          IntPtr handle,
          int minimumWorkingSetSize,
          int maximumWorkingSetSize);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, WindowLongFlags nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
          IntPtr hWnd,
          IntPtr hWndInsertAfter,
          int X,
          int Y,
          int cx,
          int cy,
          W32.SetWindowPosFlags uFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(
          uint action,
          uint uParam,
          string vParam,
          uint winIni);
        [DllImport("user32", EntryPoint = "GetWindowRect", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern long GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public extern static int GetWindowLong(IntPtr hWnd, GWL nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public extern static int SetWindowLong(IntPtr hWnd, GWL nIndex, uint dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, Int32 lParam);
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        public extern static bool ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "ShowWindow", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 ShowWindow(Int32 hwnd, Int32 nCmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        [DllImport("user32", EntryPoint = "RemoveMenu", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 RemoveMenu(Int32 hMenu, Int32 nPosition, Int32 wFlags);
        [DllImport("user32", EntryPoint = "AppendMenuA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern Int32 AppendMenu(Int32 hMenu, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);
        [DllImport("user32")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);
        [DllImport("user32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(ref Win32Point pt);
        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
            public MARGINS(int Left, int Right, int Top, int Bottom) : this()
            {
                cxLeftWidth = Left;
                cxRightWidth = Right;
                cyTopHeight = Top;
                cyBottomHeight = Bottom;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rect0;
            public RECT rect1;
            public RECT rect2;
            // Can't use an array here so simulate one
            public IntPtr lppos;
        }

        public const uint TPM_CENTERALIGN = 0x4;
        public const uint TPM_LEFTALIGN = 0x0;
        public const uint TPM_RIGHTALIGN = 0x8;
        public const uint TPM_BOTTOMALIGN = 0x20;
        public const uint TPM_TOPALIGN = 0x0;
        public const uint TPM_VCENTERALIGN = 0x10;
        public const uint TPM_LEFTBUTTON = 0x0000;
        public const uint TPM_RETURNCMD = 0x0100;
        public const uint TPM_NONOTIFY = 0x80;
        public const uint TPM_RIGHTBUTTON = 0x2;

        public const uint WM_SYSCOMMAND = 0x0112;

        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_KEYMENU = 0xF100;
        internal const int SC_RESTORE = 0xF120;
        internal const int SC_CONTEXTHELP = 0xF180;

        public static uint WS_OVERLAPPED = 0;
        public static UInt32 WS_POPUP = 0x80000000;
        public static uint WS_CHILD = 0x40000000;
        public static uint WS_MINIMIZE = 0x20000000;
        public static uint WS_VISIBLE = 0x10000000;
        public static uint WS_DISABLED = 0x8000000;
        public static uint WS_CLIPSIBLINGS = 0x4000000;
        public static uint WS_CLIPCHILDREN = 0x2000000;
        public static uint WS_MAXIMIZE = 0x1000000;
        public static uint WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
        public static uint WS_BORDER = 0x800000;
        public static uint WS_DLGFRAME = 0x400000;
        public static uint WS_VSCROLL = 0x200000;
        public static uint WS_HSCROLL = 0x100000;
        public static uint WS_SYSMENU = 0x80000;
        public static uint WS_THICKFRAME = 0x40000;
        public static uint WS_GROUP = 0x20000;
        public static uint WS_TABSTOP = 0x10000;
        public static uint WS_MINIMIZEBOX = 0x20000;
        public static uint WS_MAXIMIZEBOX = 0x10000;
        public static uint WS_TILED = WS_OVERLAPPED;
        public static uint WS_ICONIC = WS_MINIMIZE;
        public static uint WS_SIZEBOX = WS_THICKFRAME;

        public static uint WS_EX_DLGMODALFRAME = 0x0001;
        public static uint WS_EX_NOPARENTNOTIFY = 0x0004;
        public static uint WS_EX_TOPMOST = 0x0008;
        public static uint WS_EX_ACCEPTFILES = 0x0010;
        public static uint WS_EX_TRANSPARENT = 0x0020;
        public static uint WS_EX_MDICHILD = 0x0040;
        public static uint WS_EX_TOOLWINDOW = 0x0080;
        public static uint WS_EX_WINDOWEDGE = 0x0100;
        public static uint WS_EX_CLIENTEDGE = 0x0200;
        public static uint WS_EX_CONTEXTHELP = 0x0400;
        public static uint WS_EX_RIGHT = 0x1000;
        public static uint WS_EX_LEFT = 0x0000;
        public static uint WS_EX_RTLREADING = 0x2000;
        public static uint WS_EX_LTRREADING = 0x0000;
        public static uint WS_EX_LEFTSCROLLBAR = 0x4000;
        public static uint WS_EX_RIGHTSCROLLBAR = 0x0000;
        public static uint WS_EX_CONTROLPARENT = 0x10000;
        public static uint WS_EX_STATICEDGE = 0x20000;
        public static uint WS_EX_APPWINDOW = 0x40000;
        public static uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        public static uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        public static uint WS_EX_LAYERED = 0x00080000;
        public static uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        public static uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        public static uint WS_EX_COMPOSITED = 0x02000000;
        public static uint WS_EX_NOACTIVATE = 0x08000000;

        public static int WM_NCLBUTTONDOWN = 0xA1;
        public static int HTBOTTOM = 15;
        public static int HTBOTTOMLEFT = 16;
        public static int HTBOTTOMRIGHT = 17;
        public static int HTCAPTION = 2;
        public static int HTLEFT = 10;
        public static int HTRIGHT = 11;
        public static int HTTOP = 12;
        public static int HTTOPLEFT = 13;
        public static int HTTOPRIGHT = 14;

        public static int WM_KILLFOCUS = 0x0008;
        public static int WM_ACTIVE = 0x0006;
        public static int WM_SETFOCUS = 0x0007;
        public static int WM_MOUSEACTIVE = 0x0021;
        public static int WM_SETTEXT = 0x000c;
        public static int WM_CLOSE = 0x001;
        public static int WM_ENABLE = 0x000a;
        public static int WM_NOTIFY = 0x004e;
        public static int WM_STYLECHANGED = 0x007d;
        public static int WM_VSCROLL = 0x0115;
        public static int WM_HSCROLL = 0x0114;

        public enum GWL : int
        {
            ExStyle = -20,
            Style = -16
        }


        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [Flags]
        public enum BbFlags : byte
        {
            DwmBbEnable = 1,
            DwmBbBlurregion = 2,
            DwmBbTransitiononmaximized = 4,
        }

        [Flags]
        public enum DeviceContextValues : uint
        {
            Window = 1,
            Cache = 2,
            NoResetAttrs = 4,
            ClipChildren = 8,
            ClipSiblings = 16, // 0x00000010
            ParentClip = 32, // 0x00000020
            ExcludeRgn = 64, // 0x00000040
            IntersectRgn = 128, // 0x00000080
            ExcludeUpdate = 256, // 0x00000100
            IntersectUpdate = 512, // 0x00000200
            LockWindowUpdate = 1024, // 0x00000400
            UseStyle = 65536, // 0x00010000
            Validate = 2097152, // 0x00200000
        }

        [Flags]
        public enum RedrawWindowFlags : uint
        {
            Invalidate = 1,
            InternalPaint = 2,
            Erase = 4,
            Validate = 8,
            NoInternalPaint = 16, // 0x00000010
            NoErase = 32, // 0x00000020
            NoChildren = 64, // 0x00000040
            AllChildren = 128, // 0x00000080
            UpdateNow = 256, // 0x00000100
            EraseNow = 512, // 0x00000200
            Frame = 1024, // 0x00000400
            NoFrame = 2048, // 0x00000800
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            AsynWindowPos = 16384, // 0x00004000
            DeferErase = 8192, // 0x00002000
            DrawFrame = 32, // 0x00000020
            FrameChanged = DrawFrame, // 0x00000020
            HideWindow = 128, // 0x00000080
            NoActivate = 16, // 0x00000010
            NoCopyBits = 256, // 0x00000100
            NoMove = 2,
            NoOwnerZOrder = 512, // 0x00000200
            NoRedraw = 8,
            NoReposition = NoOwnerZOrder, // 0x00000200
            NoSendChanging = 1024, // 0x00000400
            NoSize = 1,
            NoZOrder = 4,
            ShowWindow = 64, // 0x00000040
        }

        public enum WindowLongFlags
        {
            GWL_USERDATA = -21, // -0x00000015
            GWL_EXSTYLE = -20, // -0x00000014
            GWL_STYLE = -16, // -0x00000010
            GWL_ID = -12, // -0x0000000C
            GWLP_HWNDPARENT = -8,
            GWLP_HINSTANCE = -6,
            GWL_WNDPROC = -4,
            DWLP_MSGRESULT = 0,
            DWLP_DLGPROC = 4,
            DWLP_USER = 8,
        }

        [Flags]
        public enum WindowStyles : uint
        {
            WS_BORDER = 8388608, // 0x00800000
            WS_CAPTION = 12582912, // 0x00C00000
            WS_CHILD = 1073741824, // 0x40000000
            WS_CLIPCHILDREN = 33554432, // 0x02000000
            WS_CLIPSIBLINGS = 67108864, // 0x04000000
            WS_DISABLED = 134217728, // 0x08000000
            WS_DLGFRAME = 4194304, // 0x00400000
            WS_GROUP = 131072, // 0x00020000
            WS_HSCROLL = 1048576, // 0x00100000
            WS_MAXIMIZE = 16777216, // 0x01000000
            WS_MAXIMIZEBOX = 65536, // 0x00010000
            WS_MINIMIZE = 536870912, // 0x20000000
            WS_MINIMIZEBOX = WS_GROUP, // 0x00020000
            WS_OVERLAPPED = 0,
            WS_POPUP = 2147483648, // 0x80000000
            WS_SIZEFRAME = 262144, // 0x00040000
            WS_SYSMENU = 524288, // 0x00080000
            WS_TABSTOP = WS_MAXIMIZEBOX, // 0x00010000
            WS_VISIBLE = 268435456, // 0x10000000
            WS_VSCROLL = 2097152, // 0x00200000
        }

        [Flags]
        public enum WindowStylesEx : uint
        {
            WS_EX_ACCEPTFILES = 16, // 0x00000010
            WS_EX_APPWINDOW = 262144, // 0x00040000
            WS_EX_CLIENTEDGE = 512, // 0x00000200
            WS_EX_COMPOSITED = 33554432, // 0x02000000
            WS_EX_CONTEXTHELP = 1024, // 0x00000400
            WS_EX_CONTROLPARENT = 65536, // 0x00010000
            WS_EX_DLGMODALFRAME = 1,
            WS_EX_LAYERED = 524288, // 0x00080000
            WS_EX_LAYOUTRTL = 4194304, // 0x00400000
            WS_EX_LEFT = 0,
            WS_EX_LEFTSCROLLBAR = 16384, // 0x00004000
            WS_EX_LTRREADING = 0,
            WS_EX_MDICHILD = 64, // 0x00000040
            WS_EX_NOACTIVATE = 134217728, // 0x08000000
            WS_EX_NOINHERITLAYOUT = 1048576, // 0x00100000
            WS_EX_NOPARENTNOTIFY = 4,
            WS_EX_OVERLAPPEDWINDOW = 768, // 0x00000300
            WS_EX_PALETTEWINDOW = 392, // 0x00000188
            WS_EX_RIGHT = 4096, // 0x00001000
            WS_EX_RIGHTSCROLLBAR = 0,
            WS_EX_RTLREADING = 8192, // 0x00002000
            WS_EX_STATICEDGE = 131072, // 0x00020000
            WS_EX_TOOLWINDOW = 128, // 0x00000080
            WS_EX_TOPMOST = 8,
            WS_EX_TRANSPARENT = 32, // 0x00000020
            WS_EX_WINDOWEDGE = 256, // 0x00000100
        }

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0,
            SMTO_BLOCK = 1,
            SMTO_ABORTIFHUNG = 2,
            SMTO_NOTIMEOUTIFNOTHUNG = 8,
            SMTO_ERRORONEXIT = 32, // 0x00000020
        }

        public struct BbStruct
        {
            public W32.BbFlags Flags;
            public bool Enable;
            public IntPtr Region;
            public bool TransitionOnMaximized;
        }

        public struct DWM_COLORIZATION_PARAMS
        {
            public int clrColor;
            public int clrAfterGlow;
            public int nIntensity;
            public int clrAfterGlowBalance;
            public int clrBlurBalance;
            public int clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        public struct Margins
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
    }
}
