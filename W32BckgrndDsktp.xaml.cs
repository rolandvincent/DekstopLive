using CefSharp.Wpf;
using static API.W32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Diagnostics;

namespace DekstopLive
{
    /// <summary>
    /// Interaction logic for W32BckgrndDsktp.xaml
    /// </summary>
    public partial class W32BckgrndDsktp : Window
    {
        public W32BckgrndDsktp()
        {
            InitializeComponent();
        }

        public enum Transision
        {
            None,
            Fade,
            FromUp,
            FromBottom,
            FromLeft,
            FromRight
        }

        public enum Slide_Type
        {
            HTML,
            Flash,
            Image,
            Video
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PictureEffect
        {
            public int WiggleRadius;
            public bool WiggleEnable;
            public bool WiggleReserveMouseXY;
            public PictureEffect(int Radius, bool ReserveMouseXY = false)
            {
                this.WiggleRadius = Radius;
                this.WiggleEnable = true;
                this.WiggleReserveMouseXY = ReserveMouseXY;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SlideInformation
        {
            public Slide_Type SlideType;
            public int SlideDuration;
            public int Volume;
            public string DataSource;
            public string DataThumbnail;
            public string DataSettings;
            public Stretch ImageStretch;
            public Brush BackgroundColor;
            public PictureEffect Effect;
            public SlideInformation(string Source, Slide_Type Type, Brush Background, int Duration = 30, Stretch ImageStrech = Stretch.UniformToFill, PictureEffect Effect = new PictureEffect())
            {
                this.SlideType = Type;
                this.DataSource = Source;
                this.SlideDuration = Duration;
                this.DataThumbnail = "";
                this.DataSettings = "";
                this.BackgroundColor = Background;
                this.ImageStretch = ImageStrech;
                this.Volume = 0;
                this.Effect = Effect;
            }
        }

        private ChromiumWebBrowser _Browser;
        private Image _Image;
        private MediaElement _Video;

        public Image ImageControl
        {
            get => _Image;
            set => _Image = value;
        }

        public MediaElement VideoControl
        {
            get => _Video;
            set => _Video = value;
        }

        public ChromiumWebBrowser BrowserControl
        {
            get => _Browser;
            set => _Browser = value;
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer;

        public void ShowSlide(SlideInformation SlideData, Transision SlideTransision = Transision.None)
        {
            /* Slide Transision
             * 
             */
            ClearElement();
            Background = SlideData.BackgroundColor;
            if (SlideData.SlideType == Slide_Type.HTML)
            {
                _Browser = new ChromiumWebBrowser();
                _Browser.Address = SlideData.DataSource;
                _Browser.LoadingStateChanged += Browser_LoadingStateChanged;
                MainContent.Children.Add(_Browser);
            }else if (SlideData.SlideType == Slide_Type.Image)
            {
                ImageControl = new Image();
                ImageSource IS = new BitmapImage(new Uri(SlideData.DataSource));
                ImageControl.Source = IS;
                ImageControl.Stretch = SlideData.ImageStretch;
                if (SlideData.Effect.WiggleEnable)
                {
                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    System.Drawing.Rectangle rect = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds;
                    dispatcherTimer.Interval = new TimeSpan(1000 / 8);
                    dispatcherTimer.IsEnabled = true;
                    dispatcherTimer.Tick += delegate
                    {
                        if (SlideData.Effect.WiggleEnable && ImageControl.Visibility == Visibility.Visible)
                        {
                            Win32Point MouseP = new Win32Point();
                            GetCursorPos(ref MouseP);

                            float wX = rect.Width;
                            float wY = rect.Height;

                            float nX = MouseP.X;
                            float nY = MouseP.Y;

                            float rX = (((float)IS.Height + 2f * SlideData.Effect.WiggleRadius) * (float)(IS.Width / IS.Height)) - (float)IS.Width;

                            float mX1 = rX * (nX / wX);
                            float mX2 = rX - mX1;
                            float mY1 = 2f * SlideData.Effect.WiggleRadius * (nY / wY);
                            float mY2 = 2f * SlideData.Effect.WiggleRadius - mY1;
                            if (SlideData.Effect.WiggleReserveMouseXY)
                                ImageControl.Margin = new Thickness(-mX2, -mY2, -mX1, -mY1);
                            else
                                ImageControl.Margin = new Thickness(-mX1, -mY1, -mX2, -mY2);
                        }
                    };
                    dispatcherTimer.Start();
                }
                MainContent.Children.Add(ImageControl);
            }else if (SlideData.SlideType == Slide_Type.Video)
            {
                VideoControl = new MediaElement();
                VideoControl.Source = new Uri(SlideData.DataSource);
                VideoControl.Volume = SlideData.Volume;
                VideoControl.LoadedBehavior = MediaState.Manual;
                VideoControl.Play();
                VideoControl.MediaEnded += delegate
                {
                    VideoControl.Position = TimeSpan.Zero;
                    VideoControl.Play();
                };
                if (SlideData.Effect.WiggleEnable)
                {
                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    System.Drawing.Rectangle rect = System.Windows.Forms.Screen.FromHandle(new WindowInteropHelper(this).Handle).Bounds;
                    dispatcherTimer.Interval = new TimeSpan(1000 / 8);
                    dispatcherTimer.IsEnabled = true;
                    dispatcherTimer.Tick += delegate
                    {
                        if (SlideData.Effect.WiggleEnable && VideoControl.Visibility == Visibility.Visible)
                        {
                            if (VideoControl.NaturalVideoWidth > 0 && VideoControl.NaturalVideoHeight > 0)
                            {
                                Win32Point MouseP = new Win32Point();
                                GetCursorPos(ref MouseP);

                                float wX = rect.Width;
                                float wY = rect.Height;

                                float nX = MouseP.X;
                                float nY = MouseP.Y;

                                float rX = ((VideoControl.NaturalVideoHeight + 2f * SlideData.Effect.WiggleRadius) *
                                (VideoControl.NaturalVideoWidth / (float)VideoControl.NaturalVideoHeight)) - VideoControl.NaturalVideoWidth;

                                float mX1 = rX * (nX / wX);
                                float mX2 = rX - mX1;
                                float mY1 = 2f * SlideData.Effect.WiggleRadius * (nY / wY);
                                float mY2 = 2f * SlideData.Effect.WiggleRadius - mY1;
                                if (SlideData.Effect.WiggleReserveMouseXY)
                                    VideoControl.Margin = new Thickness(-mX2, -mY2, -mX1, -mY1);
                                else
                                    VideoControl.Margin = new Thickness(-mX1, -mY1, -mX2, -mY2);
                            }
                        }
                    };
                    dispatcherTimer.Start();
                }
                MainContent.Children.Add(VideoControl);
            }

        }

        public void ClearElement()
        {
            dispatcherTimer = null;
            for (int i = 0; i < MainContent.Children.Count; i++)
            {
                UIElement element = MainContent.Children[i];
                if (element.GetType() == typeof(ChromiumWebBrowser))
                    ((ChromiumWebBrowser)element).Dispose();
            }
            MainContent.Children.Clear();
            GC.Collect();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            
        }
    }
}
