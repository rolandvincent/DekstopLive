using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace DekstopLive
{
    class DesktopLiveWallpaper
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, Int32 nCmdShow);
        public static IntPtr workerw;
        static MainWindow sysbck = new MainWindow();

        public static readonly string author = "Roland Vincent";
        public static readonly string version = "1.0.0.1";
        public static readonly string info = "";
        public static string FileName;
        public static int Volume = 0;

        public static void FindWorker()
        {
            IntPtr window = W32.FindWindow("Progman", (string)null);
            IntPtr result = IntPtr.Zero;
            W32.SendMessageTimeout(window, 1324U, new IntPtr(0), IntPtr.Zero, W32.SendMessageTimeoutFlags.SMTO_NORMAL, 1000U, out result);
            workerw = IntPtr.Zero;
            W32.EnumWindows((W32.EnumWindowsProc)((tophandle, topparamhandle) =>
            {
                if (W32.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", IntPtr.Zero) != IntPtr.Zero)
                    workerw = W32.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", IntPtr.Zero);
                return true;
            }), IntPtr.Zero);
        }


        public static void Show(Window window)
        {
            FindWorker();
            WindowInteropHelper IH = new WindowInteropHelper(window);
            W32.SetParent( IH.Handle, workerw);
            window.Show();
        }

        public static void Unload(Window window)
        {
            window.Close();
            ShowWindow(workerw, 0);
            ShowWindow(workerw, 1);
        }
    }
}
