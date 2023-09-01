using static API.W32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static DekstopLive.DesktopLiveWallpaper;
using Microsoft.Win32;
using System.Diagnostics;
using static DekstopLive.W32BckgrndDsktp;

namespace DekstopLive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        W32BckgrndDsktp w32Desktop;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oplg = new OpenFileDialog();
            oplg.Filter = "HTML Files|*.html|All Files|*.*";
            oplg.Multiselect = false;
            if (oplg.ShowDialog(this) == true)
            {
                if (w32Desktop == null)
                    w32Desktop = new W32BckgrndDsktp();
                w32Desktop.ShowSlide(new W32BckgrndDsktp.SlideInformation(
                    oplg.FileName, W32BckgrndDsktp.Slide_Type.HTML, new SolidColorBrush(Colors.Black)));
                ShowDesktopW();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oplg = new OpenFileDialog();
            oplg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            oplg.Multiselect = false;
            if (oplg.ShowDialog(this) == true)
            {
                if (w32Desktop == null)
                    w32Desktop = new W32BckgrndDsktp();
                w32Desktop.ShowSlide(new W32BckgrndDsktp.SlideInformation(oplg.FileName, W32BckgrndDsktp.Slide_Type.Image,
                    new SolidColorBrush(Colors.Black), -1, Stretch.UniformToFill, new W32BckgrndDsktp.PictureEffect(20)));
                ShowDesktopW();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oplg = new OpenFileDialog();
            oplg.Filter = "Video Files|*.mp4;*.wmv;*.mkv;*.avi|All Files|*.*";
            oplg.Multiselect = false;
            if (oplg.ShowDialog(this) == true)
            {
                if (w32Desktop == null)
                    w32Desktop = new W32BckgrndDsktp();
                SlideInformation slideInformation = new W32BckgrndDsktp.SlideInformation(oplg.FileName, W32BckgrndDsktp.Slide_Type.Video,
                    new SolidColorBrush(Colors.Black), -1, Stretch.UniformToFill, new W32BckgrndDsktp.PictureEffect(20));
                slideInformation.Volume = 100;
                w32Desktop.ShowSlide(slideInformation);
                ShowDesktopW();
            }
        }

        private void ShowDesktopW()
        {
            FindWorker();
            WindowInteropHelper IH = new WindowInteropHelper(w32Desktop);
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.FromHandle(IH.Handle).Bounds;
            w32Desktop.Width = rect.Width;
            w32Desktop.Height = rect.Height;
            w32Desktop.Top = w32Desktop.Left = 0;
            if (!w32Desktop.IsVisible)
                w32Desktop.Show();
            SetWindowLong(IH.Handle, WindowLongFlags.GWL_STYLE, WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN | WS_OVERLAPPED | WS_MAXIMIZE);
            SetWindowLong(IH.Handle, WindowLongFlags.GWL_EXSTYLE, WS_EX_LEFT | WS_EX_LTRREADING | WS_EX_TOOLWINDOW | WS_EX_CONTROLPARENT | WS_EX_LAYERED);
            w32Desktop.WindowState = WindowState.Maximized;
            SetParent(IH.Handle, workerw);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (w32Desktop != null)
            {
                w32Desktop.Close();
            }
            ShowWindow(workerw, 0);
            ShowWindow(workerw, 1);
            Process.GetCurrentProcess().Kill();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            WallpaperEditor WP = new WallpaperEditor();
            WP.ShowDialog();
        }
    }
}