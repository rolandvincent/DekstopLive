using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DekstopLive.VMath
{
    class Standard2D
    {
        public struct Vektor2D : IEquatable<Vektor2D>
        {
            public const float NEAR_ZERO = 0.01f;
            public float vX;
            public float vY;
            public Vektor2D(float vX, float vY)
            {
                this.vX = vX;
                this.vY = vY;
            }
            public float Length => (float) Math.Sqrt(vX * vX + vY * vY);
            public bool IsNearZero => this.Length < NEAR_ZERO;
            public Vektor2D Opposite()
            {
                return new Vektor2D(-vX, -vY);
            }
            public float Angle(bool counter_clockwise = true)
            {
                if (counter_clockwise)
                    return Length == 0 ? 0 : (float) Math.Atan2(vX, vY) - AngleToRad(90);
                else
                    return Length == 0 ? AngleToRad(360) : AngleToRad(360) - (float)Math.Atan2(vX, vY) - AngleToRad(90);
            }
            public Vektor2D Mirror(float Angle)
            {
                float fixAngle = Angle % 360f;
                return Rotate(-2f * (fixAngle - RadToAngle(this.Angle())));
            }
            public Vektor2D Rotate(float Angle)
            {
                float theta = AngleToRad(Angle);
                float cs = (float)Math.Cos(theta);
                float sn = (float)Math.Sin(theta);
                return new Vektor2D(vX * cs - vY * sn, vX * sn + vY * cs);
            }
            public Vektor2D Normalize()
            {
                return Length == 0 ? Zero() : this / Length;
            }
            public static Vektor2D Zero()
            {
                return new Vektor2D(0, 0);
            }
            public static Vektor2D operator -(Vektor2D V1, Vektor2D V2)
            {
                return new Vektor2D(V1.vX - V2.vX, V1.vY - V2.vY);
            }
            public static Vektor2D operator -(Vektor2D V)
            {
                return new Vektor2D(-V.vX, -V.vY);
            }
            public static Vektor2D operator +(Vektor2D V1, Vektor2D V2)
            {
                return new Vektor2D(V1.vX + V2.vX, V1.vY + V2.vY);
            }
            public static Vektor2D operator *(Vektor2D V1, Vektor2D V2)
            {
                return new Vektor2D(V1.vX * V2.vX, V1.vY * V2.vY);
            }
            public static Vektor2D operator /(Vektor2D V1, Vektor2D V2)
            {
                return new Vektor2D(V1.vX / V2.vX, V1.vY / V2.vY);
            }
            public static Vektor2D operator *(Vektor2D V1, float value)
            {
                return new Vektor2D(V1.vX * value, V1.vY * value);
            }
            public static Vektor2D operator *(float value, Vektor2D V1)
            {
                return new Vektor2D(value * V1.vX, value * V1.vY);
            }
            public static Vektor2D operator /(Vektor2D V1, float value)
            {
                return new Vektor2D(V1.vX / value, V1.vY / value);
            }
            public static bool operator == (Vektor2D V1, Vektor2D V2)
            {
                return V1.vX == V2.vX &&  V1.vY == V2.vY;
            }
            public static Vektor2D operator /(float value, Vektor2D V1)
            {
                return new Vektor2D(value / V1.vX, value / V1.vY);
            }
            public static bool operator !=(Vektor2D V1, Vektor2D V2)
            {
                return V1.vX != V2.vX || V1.vY != V2.vY;
            }
            public override bool Equals (object o)
            {
                return o.GetType() == typeof(Vektor2D);
            }
            public override int GetHashCode()
            {
                return vX.GetHashCode() ^ vY.GetHashCode();
            }
            public bool Equals(Vektor2D other)
            {
                return vX == other.vX && vY == other.vY;
            }
        }

        public struct Point2D
        {
            public float X;
            public float Y;
            public Point2D(float X, float Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public float GetDistance(Point2D other)
            {
                float dX = this.X - other.X;
                float dY = this.Y - other.Y;
                return (float)Math.Sqrt(dX * dX + dY * dY);
            }
            public static float GetDistance(Point2D p1, Point2D p2)
            {
                float dX = p1.X - p2.X;
                float dY = p1.Y - p2.Y;
                return (float)Math.Sqrt(dX * dX + dY * dY);
            }
            public float Direction(Point2D other)
            {
                return (float)Math.Atan2(this.X - other.X, this.Y - other.Y);
            }
            public static Point2D Zero()
            {
                return new Point2D(0, 0);
            }
            public Vektor2D ToVektor2D()
            {
                return new Vektor2D(X, Y);
            }
            public static Point2D operator -(Point2D p1, Point2D p2)
            {
                return new Point2D(p1.X - p2.X, p1.Y - p2.Y);
            }
            public static Point2D operator +(Point2D p1, Point2D p2)
            {
                return new Point2D(p1.X + p2.X, p1.Y + p2.Y);
            }
            public static Point2D operator *(Point2D p1, float value)
            {
                return new Point2D(p1.X * value, p1.Y * value);
            }
            public static Point2D operator /(Point2D p1, float value)
            {
                return new Point2D(p1.X / value, p1.Y / value);
            }
            public static bool operator ==(Point2D p1, Point2D p2)
            {
                return p1.X == p2.X && p1.Y == p2.Y;
            }
            public static bool operator !=(Point2D p1, Point2D p2)
            {
                return p1.X != p2.X || p1.Y != p2.Y;
            }
            public override bool Equals(object o)
            {
                return o.GetType() == typeof(Point2D);
            }
            public override int GetHashCode()
            {
                return this.X.GetHashCode() ^ this.Y.GetHashCode();
            }
            public bool Equals(Point2D other)
            {
                return this.X == other.X && this.Y == other.Y;
            }
        }

        public static float Min(float A, float B)
        {
            return A < B ? A : B;
        }

        public static float Max(float A, float B)
        {
            return A > B ? A : B;
        }

        public static bool IsInRange(float A, float B, float Range)
        {
            return (float)Math.Abs(A - B) < 5 ? true : false;   
        }

        public static float AngleToRad(float angle)
        {
            return angle * ((float)Math.PI / 180f);
        }

        public static float RadToAngle(float rad)
        {
            return 180f * rad / (float)Math.PI;
        }

        public static Point2D RotatePoint(float X, float Y, float Angle, Point2D PointToRotate)
        {
            float s = (float)Math.Sin(Angle);
            float c = (float)Math.Cos(Angle);

            PointToRotate.X -= X;
            PointToRotate.Y -= Y;

            float nX = PointToRotate.X * c - PointToRotate.Y * s;
            float nY = PointToRotate.X * s + PointToRotate.Y * c;

            PointToRotate.X = nX + X;
            PointToRotate.Y = nY + Y;

            return PointToRotate;
        }

    }
}
