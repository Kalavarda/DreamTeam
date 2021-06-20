using System;

namespace DreamTeam.Models
{
    public class Point
    {
        private const double MinDiff = 0.01;

        public float X { get; private set; }

        public float Y { get; private set; }

        public event Action Changed;

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
