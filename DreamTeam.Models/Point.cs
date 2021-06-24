using System;
using System.Diagnostics;

namespace DreamTeam.Models
{
    [DebuggerDisplay("{X}; {Y}")]
    public class Point
    {
        private const double MinDiff = 0.001;

        public float X { get; private set; }

        public float Y { get; private set; }

        public event Action Changed;

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point(double x, double y): this((float)x, (float)y)
        {
        }

        public float DistanceTo(Point p)
        {
            var dx = p.X - X;
            var dy = p.Y - Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public Point()
        {

        }

        public void Set(float x, float y)
        {
            var dx = MathF.Abs(x - X);
            var dy = MathF.Abs(y - Y);
            if (dx < MinDiff && dy < MinDiff)
                return;

            X = x;
            Y = y;

            Changed?.Invoke();
        }
    }
}
