using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;
using static API.W32;
using static DekstopLive.VMath.Standard2D;

namespace DekstopLive
{
    class VirtualSimulation
    {
        private static Canvas TargetCanvas;
        private static Window win;
        private static int fps = 0;
        private static int cId = 0;

        #region Ball
        public struct Ball
        {
            public Point2D Position;
            public Vektor2D Velocity;
            public Vektor2D Aclerator;
            public float Radius;
            public float Mass;
            public Color Color;
            public int Id;
            public Ball(Point2D Position, float Radius, Color Color)
            {
                this.Position = Position;
                this.Velocity = Vektor2D.Zero();
                this.Aclerator = Vektor2D.Zero();
                this.Radius = Radius;
                this.Mass = Radius * 10;
                this.Color = Color;
                this.Id = cId++;
            }
        }

        public static bool IsBallOverlap(Ball ball1, Ball ball2)
        {
            return (float)Math.Abs(ball1.Position.GetDistance(ball2.Position)) <= ball1.Radius + ball2.Radius;
        }

        private static List<Ball> Balls = new List<Ball>();
        private static List<Ball[]> BallsColPair = new List<Ball[]>();
        #endregion

        #region Properties
        public int FPS { get => fps; }
        public static int MinBallSize { get; set; } = 20;
        public static int MaxBallSize { get; set; } = 40;
        public static int NumberOfBalls { get; set; } = 20;
        public static Size SimulatorSize { get; set; } = new Size(1080, 720);
        public static Canvas Canvas
        {
            get => TargetCanvas;
            set => TargetCanvas = value;
        }
        public static Window TargertWindow
        {
            get => win;
            set => win = value;
        }
        public Point2D Location { get; set; }
        public bool IsRunning()
        {
            if (FPSCounter == null)
                return false;
            return FPSCounter.IsRunning;
        }
        #endregion

        #region Draw
        public static void DrawEllipse(Ellipse ellipse, int X, int Y)
        {
            Canvas.SetLeft(ellipse, X);
            Canvas.SetTop(ellipse, Y);
            TargetCanvas.Children.Add(ellipse);
        }

        public static void DrawRectangle(Rectangle rectangle, int X, int Y)
        {
            Canvas.SetLeft(rectangle, X);
            Canvas.SetTop(rectangle, Y);
            TargetCanvas.Children.Add(rectangle);
        }

        public static void DrawLine(Line line)
        {
            Canvas.SetLeft(line, 0);
            Canvas.SetTop(line, 0);
            TargetCanvas.Children.Add(line);
        }
        #endregion

        #region Handler
        public delegate void FPSChangedEventArgs(object sender, EventArgs e);
        public event FPSChangedEventArgs FPSChanged;
        #endregion

        public VirtualSimulation(Canvas canvas, Window w)
        {
            TargetCanvas = canvas;
            win = w;
            SimulatorSize = new Size(canvas.ActualWidth, canvas.ActualHeight);
        }

        private Stopwatch FPSCounter;
        private Stopwatch Elapsed;
        public void Initialize()
        {
            Setup();
            FPSCounter = new Stopwatch();
            Elapsed = new Stopwatch();
            Elapsed.Start();
            FPSCounter.Start();
            int FrameLoaded = 0;
            int FPSMinLimit = 10;
            int FPSMaxLimit = 90;
            while (FPSCounter.IsRunning)
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate
                {
                    var fElapsed = Elapsed.ElapsedMilliseconds < FPSMinLimit ? FPSMinLimit : Elapsed.ElapsedMilliseconds > FPSMaxLimit ? FPSMaxLimit : Elapsed.ElapsedMilliseconds;
                    Update(fElapsed / 1000f);
                    if (FPSCounter.Elapsed.Seconds > 0)
                    {
                        fps = FrameLoaded;
                        FrameLoaded = 0;
                        FPSCounter.Restart();
                        FPSChanged(this, new EventArgs());
                    }
                    else
                        FrameLoaded++;
                    Elapsed.Restart();
                }));
            }
        }

        private void Setup()
        {
            //AddBall
            Random rnd = new Random(DateTime.Now.Millisecond);
            Balls.Clear();
            for (int i = 0; i < NumberOfBalls; i++)
            {
                Ball ball = new Ball(Point2D.Zero(), rnd.Next(MinBallSize, MaxBallSize), Color.FromRgb((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));
                ball.Position.X = rnd.Next((int)Math.Floor(ball.Radius), (int)Math.Floor(SimulatorSize.Width - 2 * ball.Radius));
                ball.Position.Y = rnd.Next((int)Math.Floor(ball.Radius), (int)Math.Floor(SimulatorSize.Height - 2 * ball.Radius));
                Balls.Add(ball);
            }
            //DrawCanvas();
        }

        private void Update(float fElapsed)
        {
            Location = new Point2D((float)win.Left, (float)win.Top);
            Win32Point cPos = new Win32Point();
            GetCursorPos(ref cPos);
            Ball currentBall = Balls[0];
            currentBall.Position.X = cPos.X - Location.X;
            currentBall.Position.Y = cPos.Y - Location.Y;
            currentBall.Radius = 10;
            Balls[0] = currentBall;

            for (int a = 0; a < Balls.Count; a++)
            {
                Ball ball = Balls[a];
                ball.Aclerator = -ball.Velocity * 0.8f;
                ball.Velocity += ball.Aclerator * (float)fElapsed;
                ball.Position.X += ball.Velocity.vX * (float)fElapsed;
                ball.Position.Y += ball.Velocity.vY * (float)fElapsed;

                if (ball.Velocity.IsNearZero)
                    ball.Velocity = Vektor2D.Zero();

                if (ball.Position.X - ball.Radius < 0)
                {
                    ball.Position.X = ball.Radius;
                    ball.Velocity.vX = -ball.Velocity.vX;
                }
                if (ball.Position.Y - ball.Radius < 0)
                {
                    ball.Position.Y = ball.Radius;
                    ball.Velocity.vY = -ball.Velocity.vY;
                }
                if (ball.Position.X + ball.Radius > SimulatorSize.Width)
                {
                    ball.Position.X = (float)SimulatorSize.Width - ball.Radius;
                    ball.Velocity.vX = -ball.Velocity.vX;
                }
                if (ball.Position.Y + ball.Radius > SimulatorSize.Height)
                {
                    ball.Position.Y = (float)SimulatorSize.Height - ball.Radius;
                    ball.Velocity.vY = -ball.Velocity.vY;
                }

                Balls[a] = ball;
            }

            for (int a = 0; a < Balls.Count; a++)
            {
                for (int b = 0; b < Balls.Count; b++)
                {
                    Ball ball = Balls[a];
                    Ball targetball = Balls[b];

                    if (IsBallOverlap(ball, targetball) && a != b){

                        BallsColPair.Add(new Ball[2] { ball, targetball});

                        Debug.WriteLine($"ballX:{ball.Position.X}++ballY:{ball.Position.Y}++BallId:{ball.Id}++elapsed:{fElapsed}");//++TargetX:{targetball.Position.X}++TargetY:{targetball.Position.Y}");

                        if (a == 0)
                        {
                            Point2D pDistance = ball.Position - targetball.Position;
                            targetball.Velocity = (targetball.Position.ToVektor2D() - (new Vektor2D(cPos.X, cPos.Y) - Location.ToVektor2D())) * (MaxBallSize * MaxBallSize / (targetball.Radius * targetball.Radius) * 5);
                        }

                        float fDistance = ball.Position.GetDistance(targetball.Position);
                        float fOverlap = (fDistance - ball.Radius - targetball.Radius) * 0.5f;


                        ball.Position.X -= fOverlap * (ball.Position.X - targetball.Position.X) / fDistance;
                        ball.Position.Y -= fOverlap * (ball.Position.Y - targetball.Position.Y) / fDistance;

                        targetball.Position.X += fOverlap * (ball.Position.X - targetball.Position.X) / fDistance;
                        targetball.Position.Y += fOverlap * (ball.Position.Y - targetball.Position.Y) / fDistance;

                        if (ball.Position.X - ball.Radius < 0)
                        {
                            ball.Position.X = ball.Radius;
                            ball.Velocity.vX = -ball.Velocity.vX;
                        }
                        if (ball.Position.Y - ball.Radius < 0)
                        {
                            ball.Position.Y = ball.Radius;
                            ball.Velocity.vY = -ball.Velocity.vY;
                        }
                        if (ball.Position.X + ball.Radius > SimulatorSize.Width)
                        {
                            ball.Position.X = (float)SimulatorSize.Width - ball.Radius;
                            ball.Velocity.vX = -ball.Velocity.vX;
                        }
                        if (ball.Position.Y + ball.Radius > SimulatorSize.Height)
                        {
                            ball.Position.Y = (float)SimulatorSize.Height - ball.Radius;
                            ball.Velocity.vY = -ball.Velocity.vY;
                        }
                        if (targetball.Position.X - targetball.Radius < 0)
                        {
                            targetball.Position.X = targetball.Radius;
                            targetball.Velocity.vX = -targetball.Velocity.vX;
                        }
                        if (targetball.Position.Y - targetball.Radius < 0)
                        {
                            targetball.Position.Y = targetball.Radius;
                            targetball.Velocity.vY = -targetball.Velocity.vY;
                        }
                        if (targetball.Position.X + targetball.Radius > SimulatorSize.Width)
                        {
                            targetball.Position.X = (float)SimulatorSize.Width - targetball.Radius;
                            targetball.Velocity.vX = -targetball.Velocity.vX;
                        }
                        if (targetball.Position.Y + targetball.Radius > SimulatorSize.Height)
                        {
                            targetball.Position.Y = (float)SimulatorSize.Height - targetball.Radius;
                            targetball.Velocity.vY = -targetball.Velocity.vY;
                        }

                        Balls[a] = ball;
                        Balls[b] = targetball;
                    }
                }
            }

            for (int i = 0; i < BallsColPair.Count; i++)
            { 
                Ball b1 = Balls[BallsColPair[i][0].Id];
                Ball b2 = Balls[BallsColPair[i][1].Id];

                if (b1.Id != 0 && b2.Id != 0)
                {
                    float fDistance = b1.Position.GetDistance(b2.Position);

                    float nX = (b2.Position.X - b1.Position.X) / fDistance;
                    float nY = (b2.Position.Y - b1.Position.Y) / fDistance;

                    float tX = -nY;
                    float tY = nX;

                    float dpTan1 = b1.Velocity.vX * tX + b1.Velocity.vY * tY;
                    float dpTan2 = b2.Velocity.vX * tX + b2.Velocity.vY * tY;

                    float dpNorm1 = b1.Velocity.vX * nX + b1.Velocity.vY * nY;
                    float dpNorm2 = b2.Velocity.vX * nX + b2.Velocity.vY * nY;

                    float m1 = (dpNorm1 * (b1.Mass - b2.Mass) + 2f * b2.Mass * dpNorm2) / (b1.Mass + b2.Mass);
                    float m2 = (dpNorm2 * (b2.Mass - b1.Mass) + 2f * b2.Mass * dpNorm1) / (b1.Mass + b2.Mass);

                    b1.Velocity.vX = tX * dpTan1 + nX * m1;
                    b1.Velocity.vY = tY * dpTan1 + nY * m1;
                    b2.Velocity.vX = tX * dpTan2 + nX * m2;
                    b2.Velocity.vY = tY * dpTan2 + nY * m2;


                    Balls[b1.Id] = b1;
                    Balls[b2.Id] = b2;

                    BallsColPair.RemoveAt(i);
                }

                
            }





            DrawCanvas();
            //if (TargetCanvas.Children.Count == 0)
            //    DrawCanvas();
            //else
            //{
            //    for (int i = 0; i < Balls.Count; i++)
            //    {
            //        Ball ball = Balls[i];
            //        Ellipse  e = TargetCanvas.Children.OfType<Ellipse>().ToList<Ellipse>()[i];
            //        Canvas.SetTop(e, ball.Position.Y);
            //        Canvas.SetLeft(e, ball.Position.X);
            //        TargetCanvas.Children.OfType<Ellipse>().ToList<Ellipse>()[i] = e;
            //        Line l1 = TargetCanvas.Children.OfType<Line>().ToList().FindAll(x => (string)x.Tag == "l1-"+ball.Id).FirstOrDefault();
            //        int idx = TargetCanvas.Children.OfType<Line>().ToList().IndexOf(l1);
            //        l1.X1 = ball.Position.X;
            //        l1.Y1 = ball.Position.Y;
            //        Point2D Point2nd = RotatePoint(ball.Position.X, ball.Position.Y, ball.Velocity.Angle(false), new Point2D(ball.Position.X + ball.Radius, ball.Position.Y));
            //        l1.X2 = Point2nd.X;
            //        l1.Y2 = Point2nd.Y;
            //        TargetCanvas.Children.OfType<Line>().ToList<Line>()[idx] = l1;
            //        Line l2 = TargetCanvas.Children.OfType<Line>().ToList<Line>().Find(x => (string)x.Tag == "l2-" + ball.Id);
            //        idx = TargetCanvas.Children.OfType<Line>().ToList<Line>().IndexOf(l2);
            //        l2.X1 = ball.Position.X;
            //        l2.Y1 = ball.Position.Y;
            //        Point2D mPoint2nd = RotatePoint(ball.Position.X, ball.Position.Y, ball.Velocity.Mirror(90).Angle(false), new Point2D(ball.Position.X + ball.Radius, ball.Position.Y));
            //        l2.X2 = mPoint2nd.X;
            //        l2.Y2 = mPoint2nd.Y;
            //        TargetCanvas.Children.OfType<Line>().ToList<Line>()[idx] = l2;

            //    }
            //}
        }

        private void DrawCanvas()
        {
            //Draw
            TargetCanvas.Children.Clear();
            foreach (Ball ball in Balls)
            {
                Ellipse ellipse = new Ellipse();
                Line line = new Line();
                Line mline = new Line();
                ellipse.Fill = new SolidColorBrush(ball.Color);
                ellipse.Width = ellipse.Height = ball.Radius * 2;
                ellipse.Tag = ball.Id;
                line.Stroke = new SolidColorBrush(Color.FromRgb((byte)(255 - ball.Color.R), (byte)(255 - ball.Color.G), (byte)(255 - ball.Color.B)));
                line.X1 = ball.Position.X;
                line.Y1 = ball.Position.Y;
                line.Tag = "l1-" + ball.Id;
                mline.Stroke = new SolidColorBrush(Colors.Green);
                mline.X1 = ball.Position.X;
                mline.Y1 = ball.Position.Y;
                line.Tag = "l2-" + ball.Id;



                Point2D Point2nd = RotatePoint(ball.Position.X, ball.Position.Y, ball.Velocity.Opposite().Angle(false), new Point2D(ball.Position.X + ball.Radius, ball.Position.Y));
                line.X2 = Point2nd.X;
                line.Y2 = Point2nd.Y;
                DrawEllipse(ellipse, (int)Math.Floor(ball.Position.X - ball.Radius), (int)Math.Floor(ball.Position.Y - ball.Radius));
                DrawLine(line);
            }
        }
    }
}
