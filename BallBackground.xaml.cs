using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static DekstopLive.VMath.Standard2D;
using static API.W32;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace DekstopLive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BallBackground : Window
    {
        public BallBackground()
        {
            InitializeComponent();
        }

        private Stopwatch stopwatch = new Stopwatch();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VirtualSimulation vs = new VirtualSimulation(DCanvas, this);
            vs.FPSChanged += sss;
            vs.Initialize();
        }

        public void sss (object sender, EventArgs e)
        {
            lbl.Content = ((VirtualSimulation)sender).FPS;
        }
        //private int BanyakBola = 10;

        //List<Ball> balls = new List<Ball>();
        //List<Ball> OverlapBall = new List<Ball>();
        //int SelectedBall = 0;
        //int LastId = 0;
        HwndSource thisHandle;

        //struct Ball
        //{
        //    public float pX; //Point
        //    public float pY;
        //    public float oX; //Backup Velocity
        //    public float oY;
        //    public float vX; //Velocity
        //    public float vY;
        //    public float Angle;
        //    public float Overlap;
        //    public int Rad; //Radius
        //    public int Mass; //Massa

        //    public int Id;
        //    public Color Color;
        //}

        //private void Setup()
        //{
        //    {
        //        Ball ball = new Ball();
        //        ball.Color = Colors.Red;
        //        ball.Id = LastId++;
        //        ball.Rad = 15;
        //        ball.Mass = ball.Rad * 10;
        //        ball.pX = 0;
        //        ball.pY = 0;
        //        balls.Add(ball);
        //    }
        //    Random rand = new Random(DateTime.Now.Millisecond);
        //    for (int i = 0; i < BanyakBola; i++)
        //    {
        //        Ball ball = new Ball();
        //        ball.Color = Color.FromRgb((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255));
        //        ball.Rad = rand.Next(20, 60);
        //        ball.Mass = ball.Rad * 10;
        //        ball.pX = ball.oX = rand.Next(ball.Rad, (int)DCanvas.ActualWidth - (2 * ball.Rad));
        //        ball.pY = ball.oY = rand.Next(ball.Rad, (int)DCanvas.ActualHeight - (2 * ball.Rad));
        //        ball.Id = Marshal.SizeOf(ball);
        //        balls.Add(ball);
        //    }

        //    xCanvas = new DrawingCanvas(DCanvas);
        //    thisHandle = PresentationSource.FromVisual((Visual)DCanvas) as HwndSource;
        //    DCanvas.Cursor = Cursors.None;
        //}

        //private bool IsBallOverlap(Ball ball1, Ball ball2)
        //{
        //    float distance = (float)Math.Sqrt(Math.Pow(ball1.pX - ball2.pX, 2) + Math.Pow(ball1.pY - ball2.pY, 2));
        //    if (distance < ball1.Rad + ball2.Rad)
        //        return true;
        //    return false;
        //}

        //private void Update(float Time)
        //{
        //    Win32Point cPos = new Win32Point();
        //    GetCursorPos(ref cPos);    
        //    Ball currentBall = balls[0];
        //    currentBall.pX = cPos.X - (int)this.Left ;
        //    currentBall.pY = cPos.Y - (int)this.Top ;
        //    balls[0] = currentBall;
        //    for (int i = 0; i < balls.Count; i++)
        //    {
        //        Ball ball1 = balls[i];
        //        ball1.vX = ball1.vY = 0;
        //        balls[i] = ball1;
        //    }
        //    for (int i = 0; i < balls.Count; i++)
        //    {
        //        for (int e = i + 1; e < balls.Count; e++)
        //        {
        //            Ball ball1 = balls[i], ball2 = balls[e];
        //            float oX = ball1.pX - ball2.pX;
        //            float oY = ball1.pY - ball2.pY;
        //            float distance = (float)Math.Sqrt(Math.Pow(oX, 2) + Math.Pow(oY, 2));
        //            float overlap = ball1.Rad + ball2.Rad - distance;
        //            if (IsBallOverlap(ball1, ball2))
        //            {
        //                ball1.vX += oX * (ball1.Rad / 60f);
        //                ball1.vY += oY * (ball1.Rad / 60f);
        //                ball1.Angle = new Vektor2D(ball1.vX, ball1.vY).Opposite().Angle(false) - AngleToRad(90);
        //                ball2.vX += -oX * (ball2.Rad/60f);
        //                ball2.vY += -oY * (ball2.Rad / 60f);
        //                ball2.Angle = new Vektor2D(ball2.vX, ball2.vY).Opposite().Angle(false) - AngleToRad(90);
        //                balls[i] = ball1;
        //                balls[e] = ball2;
        //                if (ball1.Id == balls[0].Id)
        //                    lblang.Content =$"oX:{ball1.vX} & oY:{ball1.vY} & oY/oX:{oY/oX} & ball1.angle:{ball1.Angle} & Tan(oY/oX):{Math.Tan(oY / oX)} & deg(ball1.angle):{RadToAngle(ball1.Angle)}";
        //            }
        //        }
        //    }
        //}
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((System.Windows.Controls.Button)sender).Content = "DDD";
            DesktopLiveWallpaper.Show(this);
        }
    }
}
