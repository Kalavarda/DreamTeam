using System;
using System.Diagnostics;

namespace DreamTeam.Models
{
    [DebuggerDisplay("{X}; {Y}")]
    public class PointF
    {
        private const double MinDiff = 0.001;

        public float X { get; private set; }

        public float Y { get; private set; }

        public event Action Changed;

        public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public PointF(double x, double y): this((float)x, (float)y)
        {
        }

        public float DistanceTo(PointF p)
        {
            var dx = p.X - X;
            var dy = p.Y - Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public float DistanceTo(Bounds bounds)
        {
            if (bounds == null) throw new ArgumentNullException(nameof(bounds));

            var distance = DistanceTo(bounds.Center);

            if (bounds is RoundBounds round)
                return distance <= round.Radius ? 0 : distance - round.Radius;

            throw new NotImplementedException();
        }

        public PointF()
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

        /// <summary>
        /// Находит точку на прямой до точки <see cref="target"/>, на расстоянии расстояние <see cref="distance"/>
        /// </summary>
        public PointF GetPointAtLineTo(PointF target, float distance)
        {
            var dx = target.X - X;
            var dy = target.Y - Y;
            var a = MathF.Atan2(dy, dx);
            //var distance = MathF.Sqrt(dx * dx + dy * dy) - distanceBefore;
            dx = distance * MathF.Cos(a);
            dy = distance * MathF.Sin(a);
            return new PointF(X + dx, Y + dy);
        }
    }
}
